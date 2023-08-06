﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class Slider
    {
        [Key]
        public int SlideID { get; set; }

        [Display(Name = "عنوان اسلایدر")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(350, ErrorMessage = "عنوان اسلایدر حداکثر میتواند دارای 350 کاراکتر باشد")]
        public string Title { get; set; }

        [Display(Name = "تصویر اسلایدر")]
        public string ImageName { get; set; }

        [Display(Name = "تاریخ شروع نمایش")]
        public DateTime StartDate { get; set; }

        [Display(Name = "تاریخ اتمام نمایش")]
        public DateTime EndDate { get; set; }

        [Display(Name = "فعال/غیر فعال")]
        public bool IsActive { get; set; }
    }
}