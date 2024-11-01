namespace Talabat.Api.Soluation2a.Errors
{
    public class ApiValidationErrorRespone : ApiRespone
    {
        public IEnumerable<string> Errors { get; set; }
        public ApiValidationErrorRespone():base(400)
        {
            Errors = new List<string>();
        }

    }
}
