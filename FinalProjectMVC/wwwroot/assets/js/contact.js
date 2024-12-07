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