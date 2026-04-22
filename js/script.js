// ===== БУРГЕР-МЕНЮ =====
document.addEventListener('DOMContentLoaded', () => {
  const burger = document.getElementById('burger');
  const mainNav = document.getElementById('mainNav');

  if (burger && mainNav) {
    burger.addEventListener('click', () => {
      burger.classList.toggle('active');
      mainNav.classList.toggle('open');
    });

    // Закрытие меню при клике на ссылку
    const navLinks = mainNav.querySelectorAll('.nav-link');
    navLinks.forEach(link => {
      link.addEventListener('click', () => {
        burger.classList.remove('active');
        mainNav.classList.remove('open');
      });
    });

    // Закрытие меню при клике вне его
    document.addEventListener('click', (e) => {
      if (!burger.contains(e.target) && !mainNav.contains(e.target)) {
        burger.classList.remove('active');
        mainNav.classList.remove('open');
      }
    });
  }

  // ===== ФОРМА КОНТАКТОВ — ТОСТ ВМЕСТО ALERT =====
  const contactForm = document.getElementById('contactForm');

  if (contactForm) {
    contactForm.addEventListener('submit', (event) => {
      event.preventDefault();

      // Показываем красивый тост вместо alert()
      showToast(
        'Сообщение улетело в безну, пишите лучше в тг по номеру телефона 🎉',
      
      );

      // Очищаем форму
      contactForm.reset();
    });
  }

  // ===== ФУНКЦИЯ ПОКАЗА ТОСТА =====
  function showToast(title, message) {
    // Создаём оверлей
    const overlay = document.createElement('div');
    overlay.className = 'toast-overlay';

    overlay.innerHTML = `
      <div class="toast-box">
        <h3>${title}</h3>
        <p>${message}</p>
        <button class="btn btn-primary" id="toastClose">Понятно!</button>
      </div>
    `;

    document.body.appendChild(overlay);

    // Показ с анимацией
    requestAnimationFrame(() => {
      overlay.classList.add('show');
    });

    // Закрытие
    const closeBtn = overlay.querySelector('#toastClose');
    closeBtn.addEventListener('click', () => {
      overlay.classList.remove('show');
      setTimeout(() => overlay.remove(), 300);
    });

    // Закрытие по клику на оверлей
    overlay.addEventListener('click', (e) => {
      if (e.target === overlay) {
        overlay.classList.remove('show');
        setTimeout(() => overlay.remove(), 300);
      }
    });
  }

  // ===== АНИМАЦИЯ ПОЯВЛЕНИЯ ЭЛЕМЕНТОВ =====
  const animatedElements = document.querySelectorAll('.animate-on-scroll');

  if (animatedElements.length > 0) {
    const observer = new IntersectionObserver((entries) => {
      entries.forEach(entry => {
        if (entry.isIntersecting) {
          entry.target.classList.add('animate-in');
          observer.unobserve(entry.target);
        }
      });
    }, {
      threshold: 0.1,
      rootMargin: '0px 0px -50px 0px'
    });

    animatedElements.forEach(el => observer.observe(el));
  }

  // ===== ПРОГРЕСС-БАРЫ — АНИМАЦИЯ =====
  const progressBars = document.querySelectorAll('.progress-fill');

  if (progressBars.length > 0) {
    const barObserver = new IntersectionObserver((entries) => {
      entries.forEach(entry => {
        if (entry.isIntersecting) {
          const targetWidth = entry.target.style.width;
          entry.target.style.width = '0%';
          requestAnimationFrame(() => {
            requestAnimationFrame(() => {
              entry.target.style.width = targetWidth;
            });
          });
          barObserver.unobserve(entry.target);
        }
      });
    }, {
      threshold: 0.3
    });

    progressBars.forEach(bar => barObserver.observe(bar));
  }
});