using csrf_example_net.Requests;
using csrf_example_net.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;


namespace csrf_example_net.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class TransactionController : ControllerBase
    {
        private readonly ILogger<TransactionController> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private static List<Transaction> transactions = new List<Transaction>()
        {
            new Transaction { Id = 1, Amount = 100, DestinationAccount = "Account1" },
            new Transaction { Id = 2, Amount = 200, DestinationAccount = "Account2" },
            new Transaction { Id = 3, Amount = 300, DestinationAccount = "Account3" }
        };

        public TransactionController(ILogger<TransactionController> logger, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
        }


        [ProducesResponseType<Transaction>(StatusCodes.Status200OK)]
        [Authorize]
        [HttpPost("create")]
        public IActionResult CreateTransaction([FromForm] NewTransactionRq transaction) // CSRF. Accept FormData
        {
            Transaction mappedTransaction = MapNewTransactionRqToTransaction(transaction);
            transactions.Add(mappedTransaction);
            return Ok(mappedTransaction);
        }

        [IgnoreAntiforgeryToken]  //CSRF vulnerability
        [ProducesResponseType<Transaction>(StatusCodes.Status200OK)]
        [Authorize]
        [HttpPost("createVulnerable")]
        public IActionResult CreateTransactionVulnerable([FromForm] NewTransactionRq transaction) // CSRF. Accept FormData
        {
            Transaction mappedTransaction = MapNewTransactionRqToTransaction(transaction);
            transactions.Add(mappedTransaction);
            return Ok(mappedTransaction);
        }

        [ProducesResponseType<List<Transaction>>(StatusCodes.Status200OK)]
        [IgnoreAntiforgeryToken]
        [Authorize]
        [HttpGet("list")]        
        public IActionResult ListTransactions()
        {
            var test = this.HttpContext.User.Identity;
            return Ok(transactions);
        }

        private Transaction MapNewTransactionRqToTransaction(NewTransactionRq newTransactionRq)
        {
            Transaction transaction = new Transaction();
            // Map properties from NewTransactionRq to Transaction
            transaction.Id = transactions.Count + 1;
            transaction.Amount = newTransactionRq.Amount;
            transaction.DestinationAccount = newTransactionRq.DestinationAccount;
            // Add any other mapping logic here
            return transaction;
        }
    }
}
