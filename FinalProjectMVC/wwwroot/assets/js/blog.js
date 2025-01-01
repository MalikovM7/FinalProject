
document.addEventListener('DOMContentLoaded', () => {
    const searchInput = document.querySelector('.search input');
    searchInput.addEventListener('input', (event) => {
        const searchTerm = event.target.value.toLowerCase();
        const categories = document.querySelectorAll('.categories ul li');
        categories.forEach(category => {
            if (category.textContent.toLowerCase().includes(searchTerm)) {
                category.style.display = '';
            } else {
                category.style.display = 'none';
            }
        });
    });
});







document.addEventListener('DOMContentLoaded', () => {
    const playButton = document.querySelector('.play-button');
    playButton.addEventListener('click', () => {
        alert('Play video!');
    });
});






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




document.addEventListener('DOMContentLoaded', () => {
    const tabButtons = document.querySelectorAll('.tab-button');
    const tabPanes = document.querySelectorAll('.tab-pane');

    tabButtons.forEach(button => {
        button.addEventListener('click', () => {
            tabButtons.forEach(btn => btn.classList.remove('active'));
            button.classList.add('active');

            const target = button.getAttribute('data-tab');
            tabPanes.forEach(pane => {
                if (pane.id === target) {
                    pane.classList.add('active');
                } else {
                    pane.classList.remove('active');
                }
            });
        });
    });
});