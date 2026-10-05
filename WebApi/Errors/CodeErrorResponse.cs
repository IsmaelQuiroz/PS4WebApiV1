
namespace WebApi.Errors;

public class CodeErrorResponse
{
    public CodeErrorResponse(int statusCode , string message = null)
    {
        StatusCode = statusCode ;
        Message = message  ?? GetDefaultMessageStatusCode(statusCode) ; // if message is null
    }

    private string GetDefaultMessageStatusCode(int statusCode)
    {
        return statusCode switch
        {
            400 => "La solicitud enviada contiene errores",
            401 => "No tienes autorización para este recurso",
            404 => "No se encontró el item buscado",
            500 => "Se produjeron errores en el servidor", 
            _ => null
        };
    } 

    public int StatusCode { get; set; }
    public string Message { get; set; }

}
