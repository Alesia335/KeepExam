using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Keep.Model;
using Microsoft.AspNetCore.Identity;

namespace Keep.Areas.Identity.Data;

// Add profile data for application users by adding properties to the KeepUser class
public class KeepUser : IdentityUser
{
      public List<Note> Notes { get; set; }
      public List<Category> Categories { get; set; }

}

