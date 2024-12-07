window.addEventListener('scroll', function() {
    const header = document.querySelector('.site-header');
    const logo = this.document.querySelector(".logo > img");
  
  
    if (window.scrollY > 50) {
        header.style.height = '50px'
        logo.style.height ="60px"
    } else {
      header.style.height = '85px'
      logo.style.height ="80px"
    }
  });

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
    const testimonials = [
        {
            text: "Vivamus eget nibh. Etiam cursus leo vel metus. Nulla facilisi. Aenean nec eros. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia.",
            author: "John Doe",
            position: "Co-founder at Rent It",
        },
        {
            text: "Suspendisse eu nisl. Nullam ut libero. Integer dignissim consequat lectus. Class aptent taciti sociosqu ad litora torquent per conubia nostra.",
            author: "Jane Smith",
            position: "Marketing Manager",
        },
        {
            text: "Fusce ac felis sit amet ligula pharetra condimentum. Integer tincidunt. Cras dapibus. Vivamus elementum semper nisi.",
            author: "Michael Brown",
            position: "Client",
        },
    ];

    const testimonialContainer = document.querySelector(".testimonial");
    const dots = document.querySelectorAll(".dot");

    let currentTestimonial = 0;

    const updateTestimonial = (index) => {
        const { text, author, position } = testimonials[index];
        testimonialContainer.innerHTML = `
            <p>${text}</p>
            <p><strong>${author}</strong></p>
            <p>${position}</p>
        `;

        dots.forEach((dot, i) => {
            dot.classList.toggle("active", i === index);
        });
    };

    dots.forEach((dot) => {
        dot.addEventListener("click", (event) => {
            currentTestimonial = parseInt(event.target.dataset.index, 10);
            updateTestimonial(currentTestimonial);
        });
    });

    updateTestimonial(currentTestimonial);
});