// Initialize page
document.addEventListener('DOMContentLoaded', function() {
    initializePage();
});

function createFloatingDots() {
    const dotsContainer = document.getElementById('floatingDots');
    const numberOfDots = 50; // Adjust number of dots as needed
    
    for (let i = 0; i < numberOfDots; i++) {
        const dot = document.createElement('div');
        dot.className = 'dot';
        
        // Random size between 1px and 3px
        const size = Math.random() * 2 + 1;
        dot.style.width = size + 'px';
        dot.style.height = size + 'px';
        
        // Random position
        dot.style.left = Math.random() * 100 + '%';
        dot.style.top = Math.random() * 100 + '%';
        
        // Random animation delay
        dot.style.animationDelay = Math.random() * 8 + 's';
        
        // Random opacity variation
        const opacity = Math.random() * 0.4 + 0.4; // Between 0.4 and 0.8
        dot.style.opacity = opacity;
        
        dotsContainer.appendChild(dot);
    }
}

function initializePage() {
    // Create floating dots
    createFloatingDots();
    // Add click event to arrow
    const arrowDown = document.getElementById('arrowDown');
    arrowDown.addEventListener('click', function() {
        // Add a subtle click animation
        this.style.transform = 'scale(0.95)';
        setTimeout(() => {
            this.style.transform = 'scale(1)';
        }, 150);
        
        // You can add your navigation logic here
        console.log('Arrow clicked! Ready to navigate to facts section.');
    });
    
    // Add hover effects to social links
    const socialLinks = document.querySelectorAll('.social-link');
    socialLinks.forEach(link => {
        link.addEventListener('mouseenter', function() {
            this.style.transform = 'translateY(-5px) scale(1.1)';
        });
        
        link.addEventListener('mouseleave', function() {
            this.style.transform = 'translateY(0) scale(1)';
        });
    });
    
    // Contact modal functionality
    const contactButton = document.getElementById('contactButton');
    const contactModal = document.getElementById('contactModal');
    const closeModal = document.getElementById('closeModal');
    
    contactButton.addEventListener('click', function() {
        contactModal.classList.add('active');
        document.body.style.overflow = 'hidden'; // Prevent background scrolling
    });
    
    closeModal.addEventListener('click', function() {
        contactModal.classList.remove('active');
        document.body.style.overflow = 'auto'; // Restore scrolling
    });
    
    // Close modal when clicking outside
    contactModal.addEventListener('click', function(e) {
        if (e.target === contactModal) {
            contactModal.classList.remove('active');
            document.body.style.overflow = 'auto';
        }
    });
    
    // Close modal with Escape key
    document.addEventListener('keydown', function(e) {
        if (e.key === 'Escape' && contactModal.classList.contains('active')) {
            contactModal.classList.remove('active');
            document.body.style.overflow = 'auto';
        }
    });
}