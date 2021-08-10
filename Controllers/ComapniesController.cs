using Microsoft.AspNetCore.Components;
using System.Linq;

namespace MyApplication.Controllers
{
	[Microsoft.AspNetCore.Mvc.ApiController]
	[Microsoft.AspNetCore.Mvc.Route
		(template: Infrastructure.RouterConstants.Controller)]
	public class ComapniesController : Infrastructure.ApiControllerBase
	{
		#region Mock Companies
		private static System.Collections.Generic.IList<Models.Company> _companies;

		public static System.Collections.Generic.IList<Models.Company> Companies
		{
			get
			{
				if (_companies == null)
				{
					_companies =
						new System.Collections.Generic.List<Models.Company>();

					for (int index = 1; index <= 10; index++)
					{
						Models.Company company =
							new Models.Company
							{
								Id = index,
								Product = $"Product { index }",
								Companyname = $"Companyname { index }",
								Age = index ,
							};

						_companies.Add(company);
					}
				}

				return _companies;
			}
		}
		#endregion /Mock Companies

		public ComapniesController() : base()
		{
		}

		#region Get All Companies
		/// <summary>
		/// Get All Companies
		/// </summary>
		/// <returns>Companies</returns>
		[Microsoft.AspNetCore.Mvc.Route("GetAll")]
		/// 
		[Microsoft.AspNetCore.Mvc.HttpGet]

		[Microsoft.AspNetCore.Mvc.ProducesResponseType
			(type: typeof(Models.Company),
			statusCode: Microsoft.AspNetCore.Http.StatusCodes.Status200OK)]
		public
			async
			System.Threading.Tasks.Task
			<Microsoft.AspNetCore.Mvc.ActionResult<System.Collections.Generic.IList<Models.Company>>>
			Get()
		{
			System.Collections.Generic.IList<Models.Company> result = null;

			await System.Threading.Tasks.Task.Run(() =>
			{
				result =
					Companies
					.OrderBy(current => current.Companyname)
					.ToList()
					;
			});

			return Ok(value: result);
		}
		#endregion /Get All Companies

		#region Get Company By Id 
		/// <summary>
		/// Get one
		/// </summary>
		/// <returns>User</returns>
		/// 
		/// 
		/// 
		[Microsoft.AspNetCore.Mvc.Route("Get/{id}")]
		[Microsoft.AspNetCore.Mvc.HttpGet]
        //[Microsoft.AspNetCore.Mvc.HttpGet(template: "{id}")]

        [Microsoft.AspNetCore.Mvc.ProducesResponseType
            (type: typeof(Models.Company),
            statusCode: Microsoft.AspNetCore.Http.StatusCodes.Status200OK)]

        [Microsoft.AspNetCore.Mvc.ProducesResponseType
            (type: typeof(string),
            statusCode: Microsoft.AspNetCore.Http.StatusCodes.Status404NotFound)]

        public
			async
			System.Threading.Tasks.Task
			<Microsoft.AspNetCore.Mvc.ActionResult<Models.Company>>
			Get(int id)
		{
			Models.Company user = null;

			await System.Threading.Tasks.Task.Run(() =>
			{
				user =
					Companies
					.Where(current => current.Id == id)
					.FirstOrDefault();
			});

			if (user == null)
			{
				return NotFound(value: "User not found!");
			}
			else
			{
				return Ok(value: user);
			}
		}
		#endregion /Get Company By Id

		#region Create a new company
		/// <summary>
		/// Create
		/// </summary>
		/// <returns>User</returns>
		[Microsoft.AspNetCore.Mvc.Route("Create")]

		[Microsoft.AspNetCore.Mvc.HttpPost]

        [Microsoft.AspNetCore.Mvc.ProducesResponseType
            (type: typeof(Models.Company),
            statusCode: Microsoft.AspNetCore.Http.StatusCodes.Status200OK)]

        [Microsoft.AspNetCore.Mvc.ProducesResponseType
            (type: typeof(string),
            statusCode: Microsoft.AspNetCore.Http.StatusCodes.Status404NotFound)]
        public
            async
            System.Threading.Tasks.Task
            <Microsoft.AspNetCore.Mvc.ActionResult<Models.Company>>
          Post([Microsoft.AspNetCore.Mvc.FromBody]
                Models.Company viewModel)
        {
            Models.Company newUser = null;

            await System.Threading.Tasks.Task.Run(() =>
            {
                int newId =
                    Companies.Max(current => current.Id) + 1;

                newUser =
                    new Models.Company
                    {
                        Id = newId,
                        Companyname = viewModel.Companyname,
                        Product = viewModel.Product,
                        Age = viewModel.Age,
                    };

                Companies.Add(newUser);
            });

            if (newUser == null)
            {
                return NotFound(value: "Company not found!");
            }
            else
            {
                return Ok(value: newUser);
            }
        }
		#endregion /Create a new company
	}
}
