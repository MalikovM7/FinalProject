let slideIndex = 0;
let carSlideIndex = 0;

// Car slider navigation
let prevCar = document.querySelector(".btn-prev-car");
let nextCar = document.querySelector(".btn-next-car");

nextCar.addEventListener('click', () => {
    carSlideIndex++;
    showCarSlide();
});

prevCar.addEventListener('click', () => {
    carSlideIndex--;
    showCarSlide();
});

function showCarSlide() {
    const slides = document.querySelectorAll('.car-slide');

    if (carSlideIndex >= slides.length - 2) {
        carSlideIndex = 0;
    } else if (carSlideIndex < 0) {
        carSlideIndex = slides.length - 3;
    }
    const offset = -carSlideIndex * 33;
    document.querySelector('.car-slides').style.transform = `translateX(${offset}%)`;

    console.log(carSlideIndex);
}

// Main slides
let mainSlideIndex = 0;

function showMainSlides() {
    const slides = document.querySelectorAll('.slides img');
    if (mainSlideIndex >= slides.length) {
        mainSlideIndex = 0;
    } else if (mainSlideIndex < 0) {
        mainSlideIndex = slides.length - 1;
    }
    const offset = -mainSlideIndex * 100;
    document.querySelector('.slides').style.transform = `translateX(${offset}%)`;
}

function nextMainSlide() {
    mainSlideIndex++;
    showMainSlides();
}

function prevMainSlide() {
    mainSlideIndex--;
    showMainSlides();
}

// Top button
let topBtn = document.getElementById("topBtn");

window.onscroll = function () {
    scrollFunction();
};

function scrollFunction() {
    if (document.body.scrollTop > 20 || document.documentElement.scrollTop > 20) {
        topBtn.style.display = "block";
    } else {
        topBtn.style.display = "none";
    }
}

topBtn.onclick = function () {
    window.scrollTo({
        top: 0,
        behavior: 'smooth'
    });
};

// FAQ toggle
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

// Car search
document.getElementById('find-car').addEventListener('click', function () {
    alert('Searching for cars...');
});

document.getElementById('find-car-bottom').addEventListener('click', function () {
    alert('Searching for cars...');
});

// Simple slides
let simpleSlideIndex = 0;

function showSimpleSlides() {
    const slides = document.querySelectorAll('.simple-slides img');
    if (simpleSlideIndex >= slides.length) {
        simpleSlideIndex = 0;
    } else if (simpleSlideIndex < 0) {
        simpleSlideIndex = slides.length - 1;
    }
    const offset = -simpleSlideIndex * 100;
    document.querySelector('.simple-slides').style.transform = `translateX(${offset}%)`;
}

function nextSimpleSlide() {
    simpleSlideIndex++;
    showSimpleSlides();
}

function prevSimpleSlide() {
    simpleSlideIndex--;
    showSimpleSlides();
}

showSimpleSlides();


window.addEventListener('scroll', function () {
    const header = document.querySelector('.site-header');
    const logo = document.querySelector('.logo > img');

    if (window.scrollY > 50) {
        header.style.height = '50px';
        logo.style.height = '40px'; // Adjusted to be proportionate to the header height
    } else {
        header.style.height = '85px';
        logo.style.height = '60px'; // Adjusted to be proportionate to the header height
    }
});
