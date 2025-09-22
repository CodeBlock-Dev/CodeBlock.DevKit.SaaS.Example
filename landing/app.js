// Consolidated JavaScript for both index.html and page.html

// Initialize page based on current page
document.addEventListener('DOMContentLoaded', function() {
    // Check if we're on the index page (has typing animation)
    if (document.getElementById('typingText')) {
        initializeIndexPage();
    }
    
    // Check if we're on the page.html (has circular social items)
    if (document.querySelector('.circular-social')) {
        initializePagePage();
    }
    
    // Check if we're on the page.html (has new fact items)
    if (document.querySelector('.fact-item')) {
        initializeFactItems();
    }
    
    // Initialize pricing section functionality
    initializePricingSection();
    
    // Initialize testimonials section functionality
    initializeTestimonialsSection();
    
    // Initialize steps section functionality
    initializeStepsSection();
    
    // Create floating fact images for both page.html and index.html
    createFloatingFactImages();
    
    // Handle window resize to recreate the wall
    let resizeTimeout;
    window.addEventListener('resize', function() {
        clearTimeout(resizeTimeout);
        resizeTimeout = setTimeout(function() {
            const container = document.getElementById('floatingFactImages');
            if (container) {
                container.innerHTML = ''; // Clear existing images
                createFloatingFactImages(); // Recreate the wall
            }
        }, 250);
    });
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

// Page.html functionality (circular social, contact modal, scroll indicator)
function initializePagePage() {
    // Add click and touch events to scroll arrow
    const scrollArrow = document.querySelector('.scroll-arrow');
    if (scrollArrow) {
        function handleScrollClick() {
            // Add a subtle click animation
            scrollArrow.style.transform = 'scale(0.95)';
            setTimeout(() => {
                scrollArrow.style.transform = 'scale(1)';
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
        scrollArrow.addEventListener('click', handleScrollClick);
        scrollArrow.addEventListener('touchend', function(e) {
            e.preventDefault();
            handleScrollClick();
        });
        
        // Prevent default touch behavior to avoid conflicts
        scrollArrow.addEventListener('touchstart', function(e) {
            e.preventDefault();
        });
    }
    
    // Initialize circular social items
    initializeCircularSocial();
    
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

// Circular social items functionality
function initializeCircularSocial() {
    const socialItems = document.querySelectorAll('.social-item');
    
    socialItems.forEach(item => {
        // Add click animation
        item.addEventListener('click', function(e) {
            // Add click animation
            this.style.transform = 'scale(0.9)';
            setTimeout(() => {
                this.style.transform = 'scale(1.1)';
            }, 150);
            
            // Add a subtle pulse effect
            this.style.boxShadow = '0 0 20px rgba(78, 205, 196, 0.5)';
            setTimeout(() => {
                this.style.boxShadow = '';
            }, 300);
        });
        
        // Add touch support for mobile
        item.addEventListener('touchstart', function(e) {
            e.preventDefault();
            this.style.transform = 'scale(0.9)';
        });
        
        item.addEventListener('touchend', function(e) {
            e.preventDefault();
            this.style.transform = 'scale(1.1)';
        });
        
        // Add keyboard navigation support
        item.addEventListener('keydown', function(e) {
            if (e.key === 'Enter' || e.key === ' ') {
                e.preventDefault();
                this.click();
            }
        });
    });
    
    // Add intersection observer for scroll animations
    const observerOptions = {
        threshold: 0.5,
        rootMargin: '0px 0px -50px 0px'
    };
    
    const socialObserver = new IntersectionObserver((entries) => {
        entries.forEach(entry => {
            if (entry.isIntersecting) {
                // Animate social items when they come into view
                const socialItems = entry.target.querySelectorAll('.social-item');
                socialItems.forEach((item, index) => {
                    item.style.opacity = '1';
                    item.style.transform = 'scale(1)';
                    item.style.transition = `all 0.5s ease ${index * 0.1}s`;
                });
            } else {
                // Reset animation when they go out of view
                const socialItems = entry.target.querySelectorAll('.social-item');
                socialItems.forEach(item => {
                    item.style.opacity = '0.7';
                    item.style.transform = 'scale(0.8)';
                });
            }
        });
    }, observerOptions);
    
    // Observe the avatar container
    const avatarContainer = document.querySelector('.avatar-container');
    if (avatarContainer) {
        // Set initial state for animation
        const socialItems = avatarContainer.querySelectorAll('.social-item');
        socialItems.forEach(item => {
            item.style.opacity = '0.7';
            item.style.transform = 'scale(0.8)';
            item.style.transition = 'all 0.5s ease';
        });
        
        socialObserver.observe(avatarContainer);
    }
}

// Floating fact images functionality for both page.html and index.html
function createFloatingFactImages() {
    const container = document.getElementById('floatingFactImages');
    if (!container) return;
    
    // Get fact images from HTML data attributes
    const factImagesData = document.querySelectorAll('[data-fact-image]');
    const factImages = Array.from(factImagesData).map(img => img.getAttribute('data-fact-image'));
    
    // Fallback images if no data attributes are found
    if (factImages.length === 0) {
        const fallbackImages = [
            'https://images.unsplash.com/photo-1551288049-bebda4e38f71?w=300&h=200&fit=crop',
            'https://images.unsplash.com/photo-1431324155629-1a6deb1dec8d?w=300&h=200&fit=crop',
            'https://images.unsplash.com/photo-1510915361894-db8b60106cb1?w=300&h=200&fit=crop',
            'https://images.unsplash.com/photo-1551650975-87deedd944c3?w=300&h=200&fit=crop',
            'https://images.unsplash.com/photo-1461749280684-dccba630e2f6?w=300&h=200&fit=crop',
            'https://images.unsplash.com/photo-1516321318423-f06f85e504b3?w=300&h=200&fit=crop',
            'https://images.unsplash.com/photo-1504639725590-34d0984388bd?w=300&h=200&fit=crop',
            'https://images.unsplash.com/photo-1517077304055-6e89abbf09b0?w=300&h=200&fit=crop'
        ];
        factImages.push(...fallbackImages);
    }
    
    // Create a seamless tiled wall
    const imageSize = 200; // Base size for images
    const rows = Math.ceil(window.innerHeight / imageSize) + 2; // Extra rows for seamless effect
    const colsPerSet = Math.ceil(window.innerWidth / imageSize) + 2; // Extra cols for seamless effect
    
    // Create two identical sets for seamless looping
    for (let set = 0; set < 2; set++) {
        for (let row = 0; row < rows; row++) {
            for (let col = 0; col < colsPerSet; col++) {
                const image = document.createElement('img');
                image.className = 'floating-fact-image';
                
                // Randomly select an image from the fact images
                const randomImageIndex = Math.floor(Math.random() * factImages.length);
                image.src = factImages[randomImageIndex];
                image.alt = `Fact Image ${randomImageIndex + 1}`;
                
                // Calculate position
                const x = (col * imageSize) + (set * colsPerSet * imageSize);
                const y = row * imageSize;
                
                // Set size with slight variation for more natural look
                const sizeVariation = Math.random() * 40 + 20; // ±20px variation
                const width = imageSize + sizeVariation;
                const height = imageSize + sizeVariation;
                
                image.style.width = width + 'px';
                image.style.height = height + 'px';
                image.style.left = x + 'px';
                image.style.top = y + 'px';
                
                // Add slight rotation for more dynamic look
                const rotation = (Math.random() - 0.5) * 4; // ±2 degrees
                image.style.transform = `rotate(${rotation}deg)`;
                
                container.appendChild(image);
            }
        }
    }
    
    // Add some additional random images for more variety
    for (let i = 0; i < 20; i++) {
        const image = document.createElement('img');
        image.className = 'floating-fact-image';
        
        const randomImageIndex = Math.floor(Math.random() * factImages.length);
        image.src = factImages[randomImageIndex];
        image.alt = `Fact Image ${randomImageIndex + 1}`;
        
        // Random position
        const x = Math.random() * (window.innerWidth * 2);
        const y = Math.random() * window.innerHeight;
        
        // Random size
        const size = Math.random() * 100 + 150; // 150-250px
        image.style.width = size + 'px';
        image.style.height = size + 'px';
        image.style.left = x + 'px';
        image.style.top = y + 'px';
        
        // Random rotation
        const rotation = (Math.random() - 0.5) * 6; // ±3 degrees
        image.style.transform = `rotate(${rotation}deg)`;
        
        container.appendChild(image);
    }
}

// Fact items functionality with scroll animations
function initializeFactItems() {
    const factItems = document.querySelectorAll('.fact-item');
    
    if (factItems.length === 0) {
        return;
    }
    
    // Initialize fact items with scroll animations
    factItems.forEach((item, index) => {
        // Set initial state - visible but positioned off-screen
        item.style.opacity = '1';
        item.style.transition = `all 0.8s cubic-bezier(0.4, 0, 0.2, 1) ${index * 0.2}s`;
        
        // Determine layout direction based on CSS flex-direction
        const isEvenItem = index % 2 === 0;
        const factImage = item.querySelector('.fact-image');
        const factContent = item.querySelector('.fact-content');
        
        if (isEvenItem) {
            // Even items: image on left, content on right
            if (factImage) {
                factImage.style.transform = 'translateX(-80px)';
                factImage.style.transition = 'all 0.6s cubic-bezier(0.4, 0, 0.2, 1)';
                factImage.style.borderColor = 'rgba(255, 255, 255, 0.1)';
                factImage.style.boxShadow = '0 20px 40px rgba(0, 0, 0, 0.5)';
            }
            if (factContent) {
                factContent.style.transform = 'translateX(80px)';
                factContent.style.transition = 'all 0.6s cubic-bezier(0.4, 0, 0.2, 1)';
                factContent.style.borderColor = 'rgba(255, 255, 255, 0.1)';
                factContent.style.boxShadow = '0 20px 40px rgba(0, 0, 0, 0.5)';
            }
        } else {
            // Odd items: content on left, image on right (due to flex-direction: row-reverse)
            if (factContent) {
                factContent.style.transform = 'translateX(-80px)';
                factContent.style.transition = 'all 0.6s cubic-bezier(0.4, 0, 0.2, 1)';
                factContent.style.borderColor = 'rgba(255, 255, 255, 0.1)';
                factContent.style.boxShadow = '0 20px 40px rgba(0, 0, 0, 0.5)';
            }
            if (factImage) {
                factImage.style.transform = 'translateX(80px)';
                factImage.style.transition = 'all 0.6s cubic-bezier(0.4, 0, 0.2, 1)';
                factImage.style.borderColor = 'rgba(255, 255, 255, 0.1)';
                factImage.style.boxShadow = '0 20px 40px rgba(0, 0, 0, 0.5)';
            }
        }
        
        const factImageImg = item.querySelector('.fact-image img');
        if (factImageImg) {
            factImageImg.style.transition = 'opacity 0.5s ease, filter 0.5s ease, transform 0.3s ease';
        }
        
        // Add hover effects to fact-image only - using previous scroll effects
        const factImageHover = item.querySelector('.fact-image');
        if (factImageHover) {
            factImageHover.addEventListener('mouseenter', function() {
                this.style.borderColor = 'rgba(78, 205, 196, 0.6)';
                this.style.boxShadow = '0 20px 40px rgba(0, 0, 0, 0.5), 0 0 35px rgba(78, 205, 196, 0.3)';
                
                const image = this.querySelector('img');
                if (image) {
                    image.style.transform = 'scale(1.05)';
                    image.style.filter = 'brightness(1.1) contrast(1.05)';
                }
            });
            
            factImageHover.addEventListener('mouseleave', function() {
                this.style.borderColor = 'rgba(255, 255, 255, 0.1)';
                this.style.boxShadow = '0 20px 40px rgba(0, 0, 0, 0.5)';
                
                const image = this.querySelector('img');
                if (image) {
                    image.style.transform = 'scale(1)';
                    image.style.filter = 'brightness(1) contrast(1)';
                }
            });
        }
        
        // Add hover effects to fact-content only - using previous scroll effects
        const factContentHover = item.querySelector('.fact-content');
        if (factContentHover) {
            factContentHover.addEventListener('mouseenter', function() {
                this.style.borderColor = 'rgba(78, 205, 196, 0.8)';
                this.style.boxShadow = '0 20px 40px rgba(0, 0, 0, 0.5), 0 0 40px rgba(78, 205, 196, 0.4)';
            });
            
            factContentHover.addEventListener('mouseleave', function() {
                this.style.borderColor = 'rgba(255, 255, 255, 0.1)';
                this.style.boxShadow = '0 20px 40px rgba(0, 0, 0, 0.5)';
            });
        }
    });
    
    // Intersection Observer for scroll animations
    const observerOptions = {
        threshold: 0.3,
        rootMargin: '0px 0px -100px 0px'
    };
    
    const factObserver = new IntersectionObserver((entries) => {
        entries.forEach(entry => {
            console.log('Scroll effect triggered:', entry.isIntersecting); // Debug log
            
            // Get the index to determine layout
            const index = Array.from(factItems).indexOf(entry.target);
            const isEvenItem = index % 2 === 0;
            
            if (entry.isIntersecting) {
                // Slide-in animation when fact comes into view
                const factImage = entry.target.querySelector('.fact-image');
                const factContent = entry.target.querySelector('.fact-content');
                
                // Both elements slide to center position
                if (factImage) {
                    factImage.style.transform = 'translateX(0)';
                    console.log('Applied slide-in effect to fact-image'); // Debug log
                }
                
                if (factContent) {
                    factContent.style.transform = 'translateX(0)';
                    console.log('Applied slide-in effect to fact-content'); // Debug log
                }
            } else {
                // Reset animation when fact goes out of view
                const factImage = entry.target.querySelector('.fact-image');
                const factContent = entry.target.querySelector('.fact-content');
                
                if (isEvenItem) {
                    // Even items: image slides left, content slides right
                    if (factImage) {
                        factImage.style.transform = 'translateX(-80px)';
                    }
                    if (factContent) {
                        factContent.style.transform = 'translateX(80px)';
                    }
                } else {
                    // Odd items: content slides left, image slides right
                    if (factContent) {
                        factContent.style.transform = 'translateX(-80px)';
                    }
                    if (factImage) {
                        factImage.style.transform = 'translateX(80px)';
                    }
                }
            }
        });
    }, observerOptions);
    
    // Observe each fact item
    factItems.forEach((item, index) => {
        console.log(`Setting up observer for fact item ${index + 1}`);
        factObserver.observe(item);
    });
    
    console.log(`Observer setup complete for ${factItems.length} fact items`);
    
    // Add keyboard navigation
    document.addEventListener('keydown', function(e) {
        if (e.key === 'ArrowDown' || e.key === 'ArrowRight') {
            e.preventDefault();
            scrollToNextFact();
        } else if (e.key === 'ArrowUp' || e.key === 'ArrowLeft') {
            e.preventDefault();
            scrollToPreviousFact();
        }
    });
    
    // Add touch/swipe support for mobile
    let touchStartY = 0;
    let touchEndY = 0;
    
    document.addEventListener('touchstart', function(e) {
        touchStartY = e.changedTouches[0].screenY;
    });
    
    document.addEventListener('touchend', function(e) {
        touchEndY = e.changedTouches[0].screenY;
            handleSwipe();
        });
    
    function handleSwipe() {
        const swipeThreshold = 50;
        const swipeDistance = touchEndY - touchStartY;
        
        if (Math.abs(swipeDistance) > swipeThreshold) {
            if (swipeDistance > 0) {
                // Swipe down - go to previous fact
                scrollToPreviousFact();
            } else {
                // Swipe up - go to next fact
                scrollToNextFact();
            }
        }
    }
    
    function scrollToNextFact() {
        const currentFact = getCurrentVisibleFact();
        if (currentFact && currentFact.nextElementSibling) {
            currentFact.nextElementSibling.scrollIntoView({ 
                behavior: 'smooth',
                block: 'start'
            });
        }
    }
    
    function scrollToPreviousFact() {
        const currentFact = getCurrentVisibleFact();
        if (currentFact && currentFact.previousElementSibling) {
            currentFact.previousElementSibling.scrollIntoView({ 
                behavior: 'smooth',
                block: 'start'
            });
        }
    }
    
    function getCurrentVisibleFact() {
        let currentFact = null;
        factItems.forEach(item => {
            const rect = item.getBoundingClientRect();
            if (rect.top <= window.innerHeight / 2 && rect.bottom >= window.innerHeight / 2) {
                currentFact = item;
            }
        });
        return currentFact;
    }
}

// Pricing section functionality
function initializePricingSection() {
    // Handle smooth scrolling to pricing section
    const createPageBtn = document.getElementById('createPageBtn');
    if (createPageBtn) {
        createPageBtn.addEventListener('click', function(e) {
            e.preventDefault();
            const pricingSection = document.getElementById('pricing');
            if (pricingSection) {
                pricingSection.scrollIntoView({ 
                    behavior: 'smooth',
                    block: 'start'
                });
            }
        });
    }
    
    // Handle pricing plan button clicks
    const planButtons = document.querySelectorAll('.plan-btn');
    planButtons.forEach(button => {
        button.addEventListener('click', function(e) {
            e.preventDefault();
            const planTitle = this.closest('.pricing-card').querySelector('.plan-title').textContent;
            
            // Add click animation
            this.style.transform = 'scale(0.95)';
            setTimeout(() => {
                this.style.transform = 'scale(1)';
            }, 150);
            
            // Here you would typically redirect to checkout or signup
            // For now, we'll just show an alert
            alert(`You selected the ${planTitle} plan! This would redirect to checkout/signup.`);
        });
    });
    
    // Add hover effects to pricing cards
    const pricingCards = document.querySelectorAll('.pricing-card');
    pricingCards.forEach(card => {
        card.addEventListener('mouseenter', function() {
            this.style.transform = 'translateY(-5px)';
        });
        
        card.addEventListener('mouseleave', function() {
            this.style.transform = 'translateY(0)';
        });
    });
}

// Testimonials section functionality
function initializeTestimonialsSection() {
    // Add hover effects to testimonial cards
    const testimonialCards = document.querySelectorAll('.testimonial-card');
    testimonialCards.forEach(card => {
        card.addEventListener('mouseenter', function() {
            this.style.transform = 'translateY(-5px)';
        });
        
        card.addEventListener('mouseleave', function() {
            this.style.transform = 'translateY(0)';
        });
    });
    
    // Add click effects to stat items (hearts, comments, retweets)
    const statItems = document.querySelectorAll('.stat-item');
    statItems.forEach(item => {
        item.addEventListener('click', function(e) {
            e.preventDefault();
            
            // Add click animation
            this.style.transform = 'scale(0.95)';
            setTimeout(() => {
                this.style.transform = 'scale(1)';
            }, 150);
            
            // Simulate interaction (in a real app, this would update the count)
            const icon = this.querySelector('i');
            const count = this.querySelector('span');
            
            if (icon.classList.contains('bi-heart')) {
                // Heart animation
                icon.style.color = '#e74c3c';
                setTimeout(() => {
                    icon.style.color = '';
                }, 1000);
            } else if (icon.classList.contains('bi-chat')) {
                // Comment animation
                icon.style.color = '#3498db';
                setTimeout(() => {
                    icon.style.color = '';
                }, 1000);
            } else if (icon.classList.contains('bi-repeat')) {
                // Retweet animation
                icon.style.color = '#2ecc71';
                setTimeout(() => {
                    icon.style.color = '';
                }, 1000);
            }
        });
    });
    
    // Pause animation on hover for better readability
    const testimonialsTrack = document.querySelector('.testimonials-track');
    const testimonialsCarousel = document.querySelector('.testimonials-carousel');
    
    if (testimonialsTrack && testimonialsCarousel) {
        testimonialsCarousel.addEventListener('mouseenter', function() {
            testimonialsTrack.style.animationPlayState = 'paused';
        });
        
        testimonialsCarousel.addEventListener('mouseleave', function() {
            testimonialsTrack.style.animationPlayState = 'running';
        });
    }
    
    // Add touch/swipe support for mobile (optional enhancement)
    let touchStartX = 0;
    let touchEndX = 0;
    
    if (testimonialsCarousel) {
        testimonialsCarousel.addEventListener('touchstart', function(e) {
            touchStartX = e.changedTouches[0].screenX;
        });
        
        testimonialsCarousel.addEventListener('touchend', function(e) {
            touchEndX = e.changedTouches[0].screenX;
            handleTestimonialSwipe();
        });
    }
    
    function handleTestimonialSwipe() {
        const swipeThreshold = 50;
        const swipeDistance = touchEndX - touchStartX;
        
        if (Math.abs(swipeDistance) > swipeThreshold) {
            // Add a subtle visual feedback for swipe
            testimonialsTrack.style.transform = `translateX(${swipeDistance * 0.1}px)`;
            setTimeout(() => {
                testimonialsTrack.style.transform = '';
            }, 200);
        }
    }
}

// Steps section functionality
function initializeStepsSection() {
    // Only apply animations if we're NOT on the index page (i.e., on page.html)
    // Since page.html doesn't have steps section, this effectively disables animations for index.html
    const isIndexPage = document.getElementById('typingText') !== null;
    
    // Add hover effects to step items
    const stepItems = document.querySelectorAll('.step-item');
    stepItems.forEach((item, index) => {
        // Only add animation delay if not on index page
        if (!isIndexPage) {
            item.style.animationDelay = `${index * 0.1}s`;
        }
        
        // Add click interaction for mobile
        item.addEventListener('click', function() {
            // Add click animation
            this.style.transform = 'translateY(-5px) scale(0.98)';
            setTimeout(() => {
                this.style.transform = 'translateY(-10px) scale(1)';
            }, 150);
            
            // Add a subtle pulse effect
            this.style.boxShadow = '0 25px 50px rgba(0, 0, 0, 0.4)';
            setTimeout(() => {
                this.style.boxShadow = '';
            }, 300);
        });
        
        // Add touch support for mobile
        item.addEventListener('touchstart', function(e) {
            e.preventDefault();
            this.style.transform = 'translateY(-5px) scale(0.98)';
        });
        
        item.addEventListener('touchend', function(e) {
            e.preventDefault();
            this.style.transform = 'translateY(-10px) scale(1)';
        });
    });
    
    // Only add scroll animations if not on index page
    if (!isIndexPage) {
        // Add intersection observer for scroll animations
        const observerOptions = {
            threshold: 0.1,
            rootMargin: '0px 0px -50px 0px'
        };
        
        const stepsObserver = new IntersectionObserver((entries) => {
            entries.forEach(entry => {
                if (entry.isIntersecting) {
                    entry.target.style.opacity = '1';
                    entry.target.style.transform = 'translateY(0)';
                }
            });
        }, observerOptions);
        
        // Observe step items for scroll animations
        stepItems.forEach((item, index) => {
            // Set initial state for animation
            item.style.opacity = '0';
            item.style.transform = 'translateY(30px)';
            item.style.transition = `all 0.6s ease ${index * 0.1}s`;
            
            stepsObserver.observe(item);
        });
    }
    
    // Add special hover effect for step icons
    const stepIcons = document.querySelectorAll('.step-icon');
    stepIcons.forEach(icon => {
        icon.addEventListener('mouseenter', function() {
            // Add a subtle rotation effect
            this.style.transform = 'scale(1.1) rotate(5deg)';
        });
        
        icon.addEventListener('mouseleave', function() {
            this.style.transform = 'scale(1) rotate(0deg)';
        });
    });
    
    // Add click effect to step numbers
    const stepNumbers = document.querySelectorAll('.step-number');
    stepNumbers.forEach(number => {
        number.addEventListener('click', function(e) {
            e.stopPropagation();
            
            // Add click animation
            this.style.transform = 'translateX(-50%) scale(1.2)';
            this.style.boxShadow = '0 6px 20px rgba(255, 193, 7, 0.6)';
            
            setTimeout(() => {
                this.style.transform = 'translateX(-50%) scale(1)';
                this.style.boxShadow = '0 4px 15px rgba(255, 193, 7, 0.4)';
            }, 200);
        });
    });
    
    // Add keyboard navigation support
    document.addEventListener('keydown', function(e) {
        if (e.key === 'Tab') {
            // Add focus styles for accessibility
            const focusedElement = document.activeElement;
            if (focusedElement.classList.contains('step-item')) {
                focusedElement.style.outline = '2px solid #4ecdc4';
                focusedElement.style.outlineOffset = '2px';
            }
        }
    });
    
    // Remove focus outline when clicking
    stepItems.forEach(item => {
        item.addEventListener('click', function() {
            this.style.outline = 'none';
        });
    });
}
