namespace JobPortalAPI.Application.DTOs.CompanyDTOs
{
    public class CompanyUpdateDTO
    {
        public string CompanyName { get; set; }
        public string CompanyDescription { get; set; }
        public string CompanyEmail { get; set; }
        public string Industry { get; set; }
        public string WebsiteURL { get; set; }
        public string LogoURL { get; set; }
        public int EstablishedYear { get; set; }
    }

}
