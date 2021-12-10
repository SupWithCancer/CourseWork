﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    public class Mark
    {
        public int Id { get; set; }
        public int Grade { get; set; }

        public int FilmId { get; set; }

        public Film Film { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }

        
    }
}
