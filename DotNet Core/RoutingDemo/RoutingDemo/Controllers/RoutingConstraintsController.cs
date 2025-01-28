using Microsoft.AspNetCore.Mvc;

namespace RoutingDemo.Controllers
{
    [ApiController]
    [Route("api/constraints")]
    public class RoutingConstraintsController : ControllerBase
    {
        /// <summary>
        /// Type constraint: Accepts only integers in the route.
        /// Accessed via: GET api/constraints/id/{id}
        /// </summary>
        [HttpGet("id/{id:int}")]
        public IActionResult GetById(int id)
        {
            return Ok($"Received ID: {id}");
        }

        /// <summary>
        /// Range constraint: Accepts a number within a specified range (1-100).
        /// Accessed via: GET api/constraints/range/{number}
        /// </summary>
        [HttpGet("range/{number:range(1,100)}")]
        public IActionResult GetByRange(int number)
        {
            return Ok($"Number within range: {number}");
        }

        /// <summary>
        /// Length constraint: Accepts strings of a specific length (e.g., 5 characters).
        /// Accessed via: GET api/constraints/length/{code}
        /// </summary>
        [HttpGet("length/{code:length(5)}")]
        public IActionResult GetByLength(string code)
        {
            return Ok($"Code with length 5: {code}");
        }

        /// <summary>
        /// MinLength constraint: Accepts strings with a minimum length.
        /// Accessed via: GET api/constraints/minlength/{name}
        /// </summary>
        [HttpGet("minlength/{name:minlength(3)}")]
        public IActionResult GetByMinLength(string name)
        {
            return Ok($"Name with at least 3 characters: {name}");
        }

        /// <summary>
        /// MaxLength constraint: Accepts strings with a maximum length.
        /// Accessed via: GET api/constraints/maxlength/{value}
        /// </summary>
        [HttpGet("maxlength/{value:maxlength(8)}")]
        public IActionResult GetByMaxLength(string value)
        {
            return Ok($"Value with a maximum length of 8: {value}");
        }

        /// <summary>
        /// Alpha constraint: Accepts only alphabetical characters.
        /// Accessed via: GET api/constraints/alpha/{word}
        /// </summary>
        [HttpGet("alpha/{word:alpha}")]
        public IActionResult GetByAlpha(string word)
        {
            return Ok($"Alphabetical word: {word}");
        }


        /// <summary>
        /// Required constraint: Parameter is required in the route.
        /// Accessed via: GET api/constraints/required/{name}
        /// </summary>
        [HttpGet("required/{name}")]
        public IActionResult GetByRequired(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return BadRequest("Name is required.");
            }
            return Ok($"Required name: {name}");
        }

        /// <summary>
        /// Combined constraints: Accepts a valid ID within a range, and a name with minimum length.
        /// Accessed via: GET api/constraints/combined/{id}/{name}
        /// </summary>
        [HttpGet("combined/{id:int:range(1,50)}/{name:minlength(3):maxlength(10)}")]
        public IActionResult GetByCombinedConstraints(int id, string name)
        {
            return Ok($"ID: {id}, Name: {name}");
        }
    }
}
