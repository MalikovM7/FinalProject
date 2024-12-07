let slideIndex = 0;
let carSlideIndex = 0;
let prevCar = document.querySelector(".btn-prev-car")
let nextCar = document.querySelector(".btn-next-car")

nextCar.addEventListener('click',() => {
  carSlideIndex++
  showCarSlide();
})

prevCar.addEventListener('click',() => {
  carSlideIndex--
  showCarSlide();
})

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

function showCarSlide() {
  const slides = document.querySelectorAll('.car-slide');

  if (carSlideIndex >= slides.length-2) {
    carSlideIndex = 0;
  } else if (carSlideIndex < 0) {
    carSlideIndex = slides.length - 3;
  }
  const offset = -carSlideIndex * 33;
  document.querySelector('.car-slides').style.transform = `translateX(${offset}%)`;

  console.log(carSlideIndex);
}

function nextSlide() {
  slideIndex++;
  showSlides();
}

function prevSlide() {
  slideIndex--;
  showSlides();
}






let topBtn = document.getElementById("topBtn");


window.onscroll = function() {
    scrollFunction();
};

function scrollFunction() {
    if (document.body.scrollTop > 20 || document.documentElement.scrollTop > 20) {
        topBtn.style.display = "block";
    } else {
        topBtn.style.display = "none";
    }
}


topBtn.onclick = function() {
    window.scrollTo({
      top: 0,
      behavior: 'smooth'
  });
};





document.querySelectorAll('.faq-item').forEach(item => {
  item.querySelector('.faq-question').addEventListener('click', () => {
      const answer = item.querySelector('.faq-answer');
      const button = item.querySelector('.toggle-answer');
      if (answer.style.display === 'block') {
          answer.style.display = 'none';
          button.textContent = '+';
      } else {
          answer.style.display = 'block';
          button.textContent = '-';
      }
  });
});






document.getElementById('find-car').addEventListener('click', function() {
  alert('Searching for cars...');
});

document.getElementById('find-car-bottom').addEventListener('click', function() {
  alert('Searching for cars...');
});






let slideIndex1 = 0;

function showSlides() {
  const slides = document.querySelectorAll('.simple-slides img');
  if (slideIndex >= slides.length) {
    slideIndex = 0;
  } else if (slideIndex < 0) {
    slideIndex = slides.length - 1;
  }
  const offset = -slideIndex * 100;
  document.querySelector('.simple-slides').style.transform = `translateX(${offset}%)`;
}

function nextSlide() {
  slideIndex++;
  showSlides();
}

function prevSlide() {
  slideIndex--;
  showSlides();
}

showSlides();


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








const registerPasswordIcons = document.querySelectorAll(".register-password-eye");

registerPasswordIcons.forEach(icon => {
    icon.addEventListener("click", function () {
        if (this.previousElementSibling.getAttribute("type") == "password") {
            this.previousElementSibling.setAttribute("type", "text");
            this.classList.remove("fa-eye");
            this.classList.add("fa-eye-slash");
        } else {
            this.previousElementSibling.setAttribute("type", "password");
            this.classList.add("fa-eye");
            this.classList.remove("fa-eye-slash");
        }
    });
});
