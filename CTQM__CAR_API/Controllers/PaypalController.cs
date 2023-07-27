using CTQM_CAR.Domain;
using CTQM_CAR.Service.Service.Interface;
using CTQM_CAR.Shared.DTO.CarDTO;
using CTQM_CAR.Shared.DTO.CartDTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using PayPal.Api;

namespace CTQM__CAR_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaypalController : ControllerBase
    {
        private readonly IPaypalService _paypalService;
        private readonly ICartService _cartService;
        private readonly ICarService _carService;
        private readonly ILogger<PaypalController> _logger;
        private IHttpContextAccessor _httpContextAccessor;
        IConfiguration _configuration;

        public PaypalController(IPaypalService paypalService, ILogger<PaypalController> logger, IHttpContextAccessor context, IConfiguration iconfiguration, ICartService cartService, ICarService carService)
        {
            _logger = logger;
            _httpContextAccessor = context;
            _configuration = iconfiguration;
            _paypalService = paypalService;
            _cartService = cartService;
            _carService = carService;
        }
        

        [HttpGet("CreatedPayment/{guid}")]
        //public ActionResult PaymentWithPaypal(string Cancel = null, string blogId = "", string PayerID = "", string guid = "")
        public async Task<string> PaymentWithPaypal([FromRoute] Guid guid, string Cancel = null, string blogId = "", string PayerID = "")
        //public ActionResult PaymentWithPaypal([FromRoute] Guid guid, string Cancel = null, string blogId = "", string PayerID = "")
        {
            //getting the apiContext  
            var ClientID = _configuration.GetValue<string>("PayPal:Key");
            var ClientSecret = _configuration.GetValue<string>("PayPal:Secret");
            var mode = _configuration.GetValue<string>("PayPal:mode");


          
            APIContext apiContext = _paypalService.GetAPIContext(ClientID, ClientSecret, mode);

            // apiContext.AccessToken="Bearer access_token$production$j27yms5fthzx9vzm$c123e8e154c510d70ad20e396dd28287";
            try
            {
                //A resource representing a Payer that funds a payment Payment Method as paypal  
                //Payer Id will be returned when payment proceeds or click to pay  
                string payerId = PayerID;
                if (string.IsNullOrEmpty(payerId))
                {
                    
                    //this section will be executed first because PayerID doesn't exist  
                    //it is returned by the create function call of the payment class  
                    // Creating a payment  
                    // baseURL is the url on which paypal sendsback the data.  
                    string baseURI = this.Request.Scheme + "://" + this.Request.Host + "/api/Paypal/CreatedPayment/";
                    //return baseURI;
                    //here we are generating guid for storing the paymentID received in session  
                    //which will be used in the payment execution  
                    //var guidd = Convert.ToString((new Random()).Next(100000));
                    Guid guidd = Guid.NewGuid();
                    guidd = guid;
                    
                    //CreatePayment function gives us the payment approval url  
                    //on which payer is redirected for paypal account payment  
                    var createdPayment = await this.CreatePayment(apiContext, baseURI /*+ "guid="*/ + guidd, guidd, blogId);
                    
                    //get links returned from paypal in response to Create function call  
                    var links =  createdPayment.links.GetEnumerator();
                    
                    string paypalRedirectUrl = null;
                    while (links.MoveNext())
                    {
                        Links lnk = links.Current;
                        if (lnk.rel.ToLower().Trim().Equals("approval_url"))
                        {
                            //saving the payapalredirect URL to which user will be redirected for payment  
                            paypalRedirectUrl = lnk.href;
                        }
                    }
                    //return paypalRedirectUrl;
                    // saving the paymentID in the key guid  
                    _httpContextAccessor.HttpContext.Session.SetString("payment", createdPayment.id);
                    //return Redirect(paypalRedirectUrl);
                    return paypalRedirectUrl;




                }
                else
                {
                    // This function exectues after receving all parameters for the payment  

                    var paymentId = _httpContextAccessor.HttpContext.Session.GetString("payment");
                    var executedPayment = ExecutePayment(apiContext, payerId, paymentId as string);
                    //If executed payment failed then we will show payment failure message to user  
                    if (executedPayment.state.ToLower() != "approved")
                    {
                        /*return Ok(new
                        {
                            message = "PaymentFailed"
                        });*/
                        //return View("PaymentFailed");
                        return ("not ok");
                    }
                    var blogIds = executedPayment.transactions[0].item_list.items[0].sku;

                    /*return Ok(new
                    {
                        message = "PaymentSuccess"

                    });*/
                    return ("ok" );
                    //return View("PaymentSuccess");
                }
            }
            catch (Exception ex)
            {
                //return BadRequest(ex.Message);
                return ("fail" + ex);
                //return View("PaymentFailed");
            }
            //on successful payment, show success page to user.  
            return ("ok");
            //return View("SuccessView");
        }
        private PayPal.Api.Payment payment;
        private Payment ExecutePayment(APIContext apiContext, string payerId, string paymentId)
        {
            var paymentExecution = new PaymentExecution()
            {
                payer_id = payerId
            };
            this.payment = new Payment()
            {
                id = paymentId
            };
            return this.payment.Execute(apiContext, paymentExecution);
        }
        private async Task<Payment> CreatePayment(APIContext apiContext, string redirectUrl, Guid guidd, string blogId)
        {
            //create itemlist and add item objects to it  
            //var data = _cartService.GetCustomerCart(guidd);
            var data = await _cartService.GetCartById(guidd);
            var _carName = await _carService.GetCarById(data.CarId);
            var itemList = new ItemList()
            {
                items = new List<Item>()
            };
            //Adding Item Details like name, currency, price etc  
            itemList.items.Add(new Item()
            {
                name = _carName.CarName.ToString(),
                currency = "USD",
                price = data.Price.ToString(),
                quantity = data.Amount.ToString(),
                sku = "asd"
            });
            var payer = new Payer()
            {
                payment_method = "paypal"
            };
            // Configure Redirect Urls here with RedirectUrls object  
            var redirUrls = new RedirectUrls()
            {
                cancel_url = redirectUrl + "&Cancel=true",
                return_url = redirectUrl
            };
            // Adding Tax, shipping and Subtotal details  
            /*var details = new Details()
            {
                tax = "1",
                shipping = "140",
                subtotal = "1"
            };*/
            //Final amount with details  
            var amount = new Amount()
            {
                currency = "USD",
                total = data.Price.ToString(), // Total must be equal to sum of tax, shipping and subtotal.  
                //details = details
            };
            var transactionList = new List<Transaction>();
            // Adding description about the transaction  
            transactionList.Add(new Transaction()
            {
                description = "Transaction description",
                invoice_number = Guid.NewGuid().ToString(), //Generate an Invoice No  
                amount = amount,
                item_list = itemList
            });
            this.payment = new Payment()
            {
                intent = "sale",
                payer = payer,
                transactions = transactionList,
                redirect_urls = redirUrls
            };
            // Create a payment using a APIContext  
            return this.payment.Create(apiContext);
        }

    }
 }

