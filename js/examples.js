document.getElementById('portfolioMenu').addEventListener('change', function(e){
    var dropdown = document.getElementById('portfolioMenu');
    window.location.href = dropdown.options[dropdown.selectedIndex].getAttribute('id') + '.html';
});