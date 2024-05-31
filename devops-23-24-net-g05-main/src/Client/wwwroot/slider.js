function startAutoSlide() {
    var container = document.querySelector('.slide-container');
    var slides = document.querySelectorAll('.slide');
    var index = 0;

    setInterval(function () {
        index = (index + 1) % slides.length;
        container.style.transform = `translateX(-${index * 100}%)`;
    }, 5000); // Change slide every 3 seconds
}
