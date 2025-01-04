document.addEventListener("DOMContentLoaded", () => {
    // Price Filter Logic
    const priceMin = document.getElementById("priceMin");
    const priceMax = document.getElementById("priceMax");
    const priceRange = document.getElementById("priceRange");
    const filterButton = document.getElementById("filterButton");

    if (priceMin && priceMax && priceRange && filterButton) {
        // Update the displayed price range
        const updatePriceRange = () => {
            priceRange.textContent = `$${priceMin.value} - $${priceMax.value}`;
        };

        priceMin.addEventListener("input", updatePriceRange);
        priceMax.addEventListener("input", updatePriceRange);

        // Trigger filtering when clicking the filter button
        filterButton.addEventListener("click", () => {
            const minPrice = priceMin.value;
            const maxPrice = priceMax.value;

            // Redirect to the filtered URL
            const url = `/Vehicle/Vehicle?minPrice=${minPrice}&maxPrice=${maxPrice}`;
            window.location.href = url;
        });
    }

    // Testimonials Carousel Logic
    const testimonials = document.querySelectorAll(".testimonial");
    const dots = document.querySelectorAll(".dot");

    if (testimonials.length > 0 && dots.length > 0) {
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
        updateTestimonial(currentIndex);
    }
});
