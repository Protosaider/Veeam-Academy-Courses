using System;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using Common.ServiceLocator;
using DataStorage;
using DataStorage.DataProviders;
using DTO;
using Info;
using log4net;
using MessengerService.Other;

namespace MessengerService.Controllers
{
    public sealed class AuthController : ApiController
    {
        private static readonly ILog s_log = SLogger.GetLogger();
        private readonly ICUserInfoDataProvider _userDataProvider;

        public AuthController()
        {
            var container = SServiceLocator.CreateContainer();
            ConfigureContainer(ref container);
            _userDataProvider = container.Resolve<ICUserInfoDataProvider>();      
        }

        private static void ConfigureContainer(ref CContainer container)
        {
            container.Register<ICUserInfoDataProvider, CUserInfoDataProvider>(ELifeCycle.Transient);
            container.Register<CDataStorageSettings>(ELifeCycle.Transient);
        }

        //TODO Возможно ли не отображать передаваемые данные в URL?
        //[Route("api/auth/login?login={Login}&password={Password}")]
        //[Route("api/auth/login{Login}{Password}")]
        [Route("api/auth/login")]
        [ResponseType(typeof(CTokenDto))]
        [ValidateModel]
        public IHttpActionResult GetUser([FromUri]CCredentialsDto credentials)
        {
            s_log.LogInfo($"{System.Reflection.MethodBase.GetCurrentMethod()}({credentials}) is called");

            if (credentials == null)
            {
                ModelState.AddModelError($"{nameof(credentials)}", new ArgumentNullException(nameof(credentials), "Incoming data is null"));
                s_log.LogError($"{System.Reflection.MethodBase.GetCurrentMethod()}({(CCredentialsDto)null})", new ArgumentNullException(nameof(credentials), "Incoming data is null"));
                return BadRequest(ModelState);
            }

            var userInfo = _userDataProvider.GetUserByAuthData(credentials.Login, credentials.Password);

            if (userInfo == null)
            {
                s_log.LogError($"{System.Reflection.MethodBase.GetCurrentMethod()}({credentials})", new HttpResponseException(HttpStatusCode.NotFound));
                return NotFound();
            }

			var userDto = new CTokenDto(
				userInfo.Id
			);
			return Ok(userDto);
		}

        [Route("api/auth/signup")]
        [ResponseType(typeof(String))]
        [ValidateModel]
        public IHttpActionResult PostUserSignUpData([FromBody]CSignUpDto signUpData)
        {
            if (signUpData == null)
            {
                ModelState.AddModelError($"{nameof(signUpData)}", "Incoming data is null");
                s_log.LogError($"{System.Reflection.MethodBase.GetCurrentMethod()}({(CSignUpDto)null})", new ArgumentNullException(nameof(signUpData), "Incoming data is null"));
                return BadRequest(ModelState);
            }

            CUserInfo userInfo = new CUserInfo(
                Guid.Empty,
                signUpData.Credentials.Login,
                signUpData.Credentials.Password,
                default(DateTimeOffset),
                0,
                signUpData.Avatar
                );

            Guid newUserId = _userDataProvider.CreateUser(userInfo);

            if (newUserId == Guid.Empty)
            {
                s_log.LogError($"{System.Reflection.MethodBase.GetCurrentMethod()}({signUpData})", new Exception("Failed to create new user"));
                return InternalServerError();
            }

            return Ok("Signed Up");
            //return Created("api/auth/signup", newUserId);
        }
    }
}