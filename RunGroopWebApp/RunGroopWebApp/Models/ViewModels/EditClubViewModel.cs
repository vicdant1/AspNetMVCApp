﻿using RunGroopWebApp.Data.Enums;

namespace RunGroopWebApp.Models.ViewModels
{
    public class EditClubViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int? AddressId { get; set; }
        public Address? Address { get; set; }
        public string? Url { get; set; }
        public EClubCategory Category { get; set; }
    }
}
