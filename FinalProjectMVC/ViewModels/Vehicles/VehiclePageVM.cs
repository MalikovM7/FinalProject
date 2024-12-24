﻿using Domain.Models;

namespace FinalProjectMVC.ViewModels.Vehicles
{
    public class VehiclePageVM
    {

        public IEnumerable<Car> Cars { get; set; }

        public IEnumerable<Testimonial> Testimonials { get; set; }

    }
}
