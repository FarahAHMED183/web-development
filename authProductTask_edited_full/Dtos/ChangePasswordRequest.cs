namespace CRUD_Operations.Dtos
{
    public class ChangePasswordRequest
    {
        public string Session { get; set; }
        public string NewPassword { get; set; }
    }
}
