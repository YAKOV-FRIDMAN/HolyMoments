using Grpc.Core;
using GrpcShared;
using HolyMomentsGrpc.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HolyMomentsGrpc.Services
{
    public class AccountService : AccountServiceBasic.AccountServiceBasicBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public override async Task<LoginReply> Login(GrpcShared.LoginRequest request, ServerCallContext context)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user == null)
            {
                return new LoginReply
                {
                    Success = false
                };
            }


            var result = await _signInManager.PasswordSignInAsync(request.Email, request.Password, isPersistent: false, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                // get rolws
                var roles = await _userManager.GetRolesAsync(user);
                // set authClaims
                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(ClaimTypes.NameIdentifier, user.Id)

                };
                foreach (var userRole in roles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }
                // crate token
                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("ByYM000OLlMQG6VVVp1OH7Xzyr7gHuw1qvUC5dcGt3SNM"));
                var token = new JwtSecurityToken(
                                       issuer: "",
                                       audience: "",
                                       expires: DateTime.Now.AddHours(3),
                                       claims: authClaims,
                                       signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                                       );
                return new LoginReply
                {
                    Success = result.Succeeded,
                    Token = new JwtSecurityTokenHandler().WriteToken(token)
                };



            }

            return new LoginReply
            {
                Success = result.Succeeded
            };
        }


        public override async Task<RegisterReply> Register(GrpcShared.RegisterRequest request, ServerCallContext context)
        {

            var userExists = await _userManager.FindByNameAsync(request.Email);
            if (userExists != null)
            {

            }

            // יצירת מופע חדש של משתמש
            ApplicationUser user = new ApplicationUser()
            {
                Email = request.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = request.Email,
                FullName = request.FullName,
            };


            // ניסיון להוסיף את המשתמש למסד הנתונים עם הסיסמה שסופקה
            var result = await _userManager.CreateAsync(user, request.Password);

            if (result.Succeeded)
            {
                // אם ההרשמה הצליחה, אנו מחזירים תשובה חיובית
                return new RegisterReply
                {
                    Success = true,
                    Message = "User created successfully!"
                };
            }

            // במקרה של כישלון (לדוגמה, אם האימייל כבר קיים או הסיסמה אינה תואמת את הדרישות),
            // אנו יכולים להחזיר תשובה שלילית ואולי גם הודעת שגיאה
            // בפועל, כדאי להוסיף מידע נוסף על הסיבה לכישלון, אך עליך להיות זהיר לא לחשוף מידע רגיש
            return new RegisterReply
            {
                Success = false,
                Message = $"User creation failed! {result.Errors?.FirstOrDefault()?.Description}"

            };
        }

    }
}
