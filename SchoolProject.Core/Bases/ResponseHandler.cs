using Microsoft.Extensions.Localization;
using SchoolProject.Core.Resources;

namespace SchoolProject.Core.Bases
{
	public class ResponseHandler
	{
		private readonly IStringLocalizer<ShearedResources> _stringLocalizer;
		public ResponseHandler(IStringLocalizer<ShearedResources> stringLocalizer)
		{
			_stringLocalizer = stringLocalizer;
		}
		public Response<T> Deleted<T>(string Message = null)
		{
			return new Response<T>()
			{
				StatusCode = System.Net.HttpStatusCode.OK,
				Succeeded = true,
				Message = Message == null ? _stringLocalizer[ShearedResourcesKeys.Deleted] : Message
			};
		}
		public Response<T> Success<T>(T entity, object Meta = null)
		{
			return new Response<T>()
			{
				Data = entity,
				StatusCode = System.Net.HttpStatusCode.OK,
				Succeeded = true,
				Message = $"{_stringLocalizer[ShearedResourcesKeys.Succesed]}",
				Meta = Meta
			};
		}
		public Response<T> Updated<T>(string Message = null, object Meta = null)
		{
			return new Response<T>()
			{
				StatusCode = System.Net.HttpStatusCode.OK,
				Succeeded = true,
				Message = Message == null ? _stringLocalizer[ShearedResourcesKeys.Updated] : Message,
				Meta = Meta
			};
		}
		public Response<T> Unauthorized<T>()
		{
			return new Response<T>()
			{
				StatusCode = System.Net.HttpStatusCode.Unauthorized,
				Succeeded = true,
				Message = "UnAuthorized"
			};
		}
		public Response<T> BadRequest<T>(string Message = null)
		{
			return new Response<T>()
			{
				StatusCode = System.Net.HttpStatusCode.BadRequest,
				Succeeded = false,
				Message = Message == null ? "Bad Request" : Message
			};
		}

		public Response<T> UnprocessableEntity<T>(string Message = null)
		{
			return new Response<T>()
			{
				StatusCode = System.Net.HttpStatusCode.UnprocessableEntity,
				Succeeded = false,
				Message = Message == null ? "UnprocessableEntity" : Message
			};
		}

		public Response<T> NotFound<T>(string message = null)
		{
			return new Response<T>()
			{
				StatusCode = System.Net.HttpStatusCode.NotFound,
				Succeeded = false,
				Message = message == null ? _stringLocalizer[ShearedResourcesKeys.NotFound] : message
			};
		}

		public Response<T> Created<T>(T entity, object Meta = null)
		{
			return new Response<T>()
			{
				Data = entity,
				StatusCode = System.Net.HttpStatusCode.Created,
				Succeeded = true,
				Message = $"{_stringLocalizer[ShearedResourcesKeys.Created]}",
				Meta = Meta
			};
		}
	}

}
