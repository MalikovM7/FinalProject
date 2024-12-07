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






let slideIndex = 0;

function showSlides() {
  const slides = document.querySelectorAll('.slides img');
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