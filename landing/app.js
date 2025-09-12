// Consolidated JavaScript for both index.html and page.html

// Initialize page based on current page
document.addEventListener('DOMContentLoaded', function() {
    // Check if we're on the index page (has typing animation)
    if (document.getElementById('typingText')) {
        initializeIndexPage();
    }
    
    // Check if we're on the page.html (has social links and contact modal)
    if (document.getElementById('contactButton')) {
        initializePagePage();
    }
    
    // Create floating dots for both pages
    createFloatingDots();
});

// Index page functionality (typing animation)
function initializeIndexPage() {
    // Typing animation
    function getTipsFromHTML() {
        const tipsContainer = document.getElementById('tipsData');
        const tipElements = tipsContainer.querySelectorAll('[data-tip]');
        return Array.from(tipElements).map(el => el.getAttribute('data-tip'));
    }

    const tips = getTipsFromHTML();
    let currentTipIndex = 0;
    let currentCharIndex = 0;
    let isDeleting = false;
    let typingSpeed = 10;
    let deletingSpeed = 15;
    let pauseTime = 1000;

    function typeText() {
        const textElement = document.getElementById('text');
        const currentTip = tips[currentTipIndex];

        if (isDeleting) {
            textElement.textContent = currentTip.substring(0, currentCharIndex - 1);
            currentCharIndex--;
            typingSpeed = deletingSpeed;
        } else {
            textElement.textContent = currentTip.substring(0, currentCharIndex + 1);
            currentCharIndex++;
            typingSpeed = 100;
        }

        if (!isDeleting && currentCharIndex === currentTip.length) {
            typingSpeed = pauseTime;
            isDeleting = true;
        } else if (isDeleting && currentCharIndex === 0) {
            isDeleting = false;
            currentTipIndex = (currentTipIndex + 1) % tips.length;
            typingSpeed = 500;
        }

        setTimeout(typeText, typingSpeed);
    }

    // Start typing animation
    typeText();
}

// Page.html functionality (social links, contact modal, arrow)
function initializePagePage() {
    // Add click and touch events to arrow down
    const arrowDown = document.getElementById('arrowDown');
    if (arrowDown) {
        function handleArrowDownClick() {
            // Add a subtle click animation
            arrowDown.style.transform = 'scale(0.95)';
            setTimeout(() => {
                arrowDown.style.transform = 'scale(1)';
            }, 150);
            
            // Smooth scroll to facts section
            const factsSection = document.getElementById('facts');
            if (factsSection) {
                factsSection.scrollIntoView({ 
                    behavior: 'smooth',
                    block: 'start'
                });
            }
        }
        
        // Add both click and touch events for better mobile support
        arrowDown.addEventListener('click', handleArrowDownClick);
        arrowDown.addEventListener('touchend', function(e) {
            e.preventDefault();
            handleArrowDownClick();
        });
        
        // Prevent default touch behavior to avoid conflicts
        arrowDown.addEventListener('touchstart', function(e) {
            e.preventDefault();
        });
    }
    
    // Add click and touch events to arrow up
    const arrowUp = document.getElementById('arrowUp');
    if (arrowUp) {
        function handleArrowUpClick() {
            // Add a subtle click animation
            arrowUp.style.transform = 'scale(0.95)';
            setTimeout(() => {
                arrowUp.style.transform = 'scale(1)';
            }, 150);
            
            // Smooth scroll back to hero section
            const heroSection = document.getElementById('hero');
            if (heroSection) {
                heroSection.scrollIntoView({ 
                    behavior: 'smooth',
                    block: 'start'
                });
            }
        }
        
        // Add both click and touch events for better mobile support
        arrowUp.addEventListener('click', handleArrowUpClick);
        arrowUp.addEventListener('touchend', function(e) {
            e.preventDefault();
            handleArrowUpClick();
        });
        
        // Prevent default touch behavior to avoid conflicts
        arrowUp.addEventListener('touchstart', function(e) {
            e.preventDefault();
        });
    }
    
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
    
    if (contactButton && contactModal && closeModal) {
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
}

// Floating dots functionality (shared between both pages)
function createFloatingDots() {
    // Create dots for hero section
    const heroDotsContainer = document.getElementById('floatingDots');
    if (heroDotsContainer) {
        createDotsForContainer(heroDotsContainer, 50);
    }
    
    // Create dots for facts section
    const factsDotsContainer = document.getElementById('factsFloatingDots');
    if (factsDotsContainer) {
        createDotsForContainer(factsDotsContainer, 50);
    }
}

function createDotsForContainer(container, numberOfDots) {
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
        
        // Random animation duration
        dot.style.animationDuration = (Math.random() * 4 + 8) + 's';
        
        // Random opacity variation
        const opacity = Math.random() * 0.4 + 0.4; // Between 0.4 and 0.8
        dot.style.opacity = opacity;
        
        container.appendChild(dot);
    }
}
