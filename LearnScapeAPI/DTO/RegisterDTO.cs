using System.ComponentModel.DataAnnotations;

namespace LearnScapeAPI.DTO
{
    public class RegisterDTO
    {
        [Required]
        public string DisplayName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        /// <summary>
        /// This regular expression match can be used for validating strong password. 
        /// It expects atleast 1 small-case letter, 1 Capital letter, 1 digit, 1 special character and the length should be between 6-10 characters. The sequence of the characters is not important. 
        /// This expression follows the above 4 norms specified by microsoft for a strong password.
        /// </summary>
        [Required]
        [RegularExpression("(?=^.{6,10}$)(?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!@#$%^&amp;*()_+}{&quot;:;'?/&gt;.&lt;,])(?!.*\\s).*$", ErrorMessage = "Password must have atleast 1 Uppercase, 1 lowercase, 1 number, 1 non alphanumeric and 6-10 characters")]
        public string Password { get; set; }
    }
}
