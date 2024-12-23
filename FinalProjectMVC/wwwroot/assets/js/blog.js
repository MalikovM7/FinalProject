// Search filter functionality
document.addEventListener('DOMContentLoaded', () => {
    const searchInput = document.querySelector('.search input');
    const categories = document.querySelectorAll('.categories ul li');

    searchInput.addEventListener('input', (event) => {
        const searchTerm = event.target.value.toLowerCase();
        categories.forEach(category => {
            category.style.display = category.textContent.toLowerCase().includes(searchTerm) ? '' : 'none';
        });
    });
});

// Play button click handler
document.addEventListener('DOMContentLoaded', () => {
    const playButton = document.querySelector('.play-button');
    if (playButton) {
        playButton.addEventListener('click', () => {
            alert('Play video!');
        });
    }
});

// Slide show functionality
let slideIndex = 0;

function showSlides() {
    const slides = document.querySelectorAll('.slides img');
    if (!slides.length) return;

    if (slideIndex >= slides.length) {
        slideIndex = 0;
    } else if (slideIndex < 0) {
        slideIndex = slides.length - 1;
    }
    const offset = -slideIndex * 100;
    document.querySelector('.slides').style.transform = `translateX(${offset}%)`;
}

function nextSlide() {
    slideIndex++;
    showSlides();
}

function prevSlide() {
    slideIndex--;
    showSlides();
}

// Pagination functionality
document.addEventListener('DOMContentLoaded', () => {
    const buttons = document.querySelectorAll('.pagination-btn');
    let currentPage = 1;

    buttons.forEach(button => {
        button.addEventListener('click', (event) => {
            const page = event.target.getAttribute('data-page');

            if (page) {
                currentPage = parseInt(page);
            } else if (event.target.id === 'prev' && currentPage > 1) {
                currentPage--;
            } else if (event.target.id === 'next' && currentPage < buttons.length - 2) {
                currentPage++;
            }

            updateActiveButton();
        });
    });

    function updateActiveButton() {
        buttons.forEach(button => {
            button.classList.remove('active');
            if (button.getAttribute('data-page') == currentPage) {
                button.classList.add('active');
            }
        });
    }

    updateActiveButton();
});

// Tab functionality
document.addEventListener('DOMContentLoaded', () => {
    const tabButtons = document.querySelectorAll('.tab-button');
    const tabPanes = document.querySelectorAll('.tab-pane');

    tabButtons.forEach(button => {
        button.addEventListener('click', () => {
            // Deactivate all buttons and panes
            tabButtons.forEach(btn => btn.classList.remove('active'));
            tabPanes.forEach(pane => pane.classList.remove('active'));

            // Activate the clicked button and corresponding pane
            button.classList.add('active');
            const target = button.getAttribute('data-tab');
            const activePane = document.getElementById(target);
            if (activePane) {
                activePane.classList.add('active');
            }
        });
    });
});
