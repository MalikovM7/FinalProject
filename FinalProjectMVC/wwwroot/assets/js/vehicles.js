
  document.addEventListener("DOMContentLoaded", () => {
    // Update Price Range Display
    const priceMin = document.getElementById("priceMin");
    const priceMax = document.getElementById("priceMax");
    const priceRange = document.getElementById("priceRange");

    const updatePriceRange = () => {
        const min = Math.min(priceMin.value, priceMax.value);
        const max = Math.max(priceMin.value, priceMax.value);
        priceRange.textContent = `$${min} - $${max}`;
    };

    priceMin.addEventListener("input", updatePriceRange);
    priceMax.addEventListener("input", updatePriceRange);

    // Testimonials Carousel
      document.addEventListener("DOMContentLoaded", () => {
          const testimonials = document.querySelectorAll(".testimonial");
          const dots = document.querySelectorAll(".dot");

          let currentIndex = 0;

          const updateTestimonial = (index) => {
              // Hide all testimonials
              testimonials.forEach((testimonial) => {
                  testimonial.style.display = "none";
              });

              // Remove active class from all dots
              dots.forEach((dot) => {
                  dot.classList.remove("active");
              });

              // Show the selected testimonial and activate the corresponding dot
              testimonials[index].style.display = "block";
              dots[index].classList.add("active");

              currentIndex = index;
          };

          // Add click event to dots
          dots.forEach((dot) => {
              dot.addEventListener("click", (event) => {
                  const index = parseInt(event.target.dataset.index, 10);
                  updateTestimonial(index);
              });
          });

          // Show the first testimonial on load
          if (testimonials.length > 0) {
              updateTestimonial(currentIndex);
          }
      });