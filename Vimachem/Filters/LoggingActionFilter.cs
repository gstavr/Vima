using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;
using Vimachem.Models.API;
using Vimachem.Models.Domain;
using Newtonsoft.Json;
using Vimachem.Data;

namespace Vimachem.Filters
{
    public class LoggingActionFilter : IActionFilter
    {
        private readonly ILogger<LoggingActionFilter> _logger;
        private readonly ApplicationDbContext _dbContext;

        public LoggingActionFilter(ILogger<LoggingActionFilter> logger, ApplicationDbContext db)
        {
            _logger = logger;
            _dbContext = db;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {   
            _logger.LogInformation("Entering action {ActionName} of controller {ControllerName}",
                context.ActionDescriptor.RouteValues["action"], context.ActionDescriptor.RouteValues["controller"]);
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            var entityType = context.RouteData.Values["controller"];
            var actionName = context.RouteData.Values["action"];
            var timestamp = DateTime.Now;
            var entityId = context.HttpContext.Request.RouteValues["id"]; 
            var actionType = context.HttpContext.Request.Method; 
            var userId = context.HttpContext.User.Identity?.Name;
            var errorMessage = context?.Exception?.Message;

            if (context.Exception != null && !context.ExceptionHandled)
            {
                errorMessage = context.Exception.Message;

                _logger.LogError(context.Exception, "Exception thrown in action {ActionName} of controller {ControllerName}",
                    actionName, entityType);

                var apiResponse = new APIResponce
                {
                    IsSuccess = false,
                    StatusCode = HttpStatusCode.InternalServerError,
                    //TODO :Add Bellow or This one?  ErrorMessages = new List<string> { context.Exception.Message }
                    ErrorMessages = new List<string> { "Error performing the action" } 
                };

                context.Result = new ObjectResult(apiResponse)
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError
                };

                context.ExceptionHandled = true; 
            }

            _logger.LogInformation("Exiting action {ActionName} of controller {ControllerName}",
                actionName, entityType);
            
            var message = $"Action executed - {actionName} on {entityType} at {timestamp}";

            if (context.Result is ObjectResult result)
            {
                var entity = result.Value;
                message += $", Entity Details: {entity}";
            }

            _logger.LogInformation(message);
            var auditLog = new AuditLog
            {
                ActionType = actionType,
                EntityType = JsonConvert.SerializeObject(entityType),
                EntityId = entityId?.ToString(),
                Timestamp = timestamp,
                Changes = SerializeChanges(context),
                UserId = userId,
                ErrorMessage = errorMessage
            };

            try
            {
                _dbContext.AuditLog.Add(auditLog);
                _dbContext.SaveChangesAsync();
            }
            catch
            {
                _logger.LogError(message: "Error on saving to the logs");
            }
            
        }

        private string SerializeChanges(ActionExecutedContext context)
        {   
            if (context.Result is ObjectResult objectResult)
            {
                return JsonConvert.SerializeObject(objectResult.Value);
            }
            return string.Empty;
        }
    }

}
