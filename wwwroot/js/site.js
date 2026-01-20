// site.js — single, clean Article Popup modal

(function () {
  // Sadece makale linkleri
  document.addEventListener("click", async function (e) {
    const link = e.target.closest("a.js-article-popup");
    if (!link) return;

    e.preventDefault();

    const slug = link.getAttribute("data-slug");
    const fallbackHref = link.getAttribute("href") || "/Makaleler";

    if (!slug) {
      window.location.href = fallbackHref;
      return;
    }

    const url = `/Makaleler/Popup?slug=${encodeURIComponent(slug)}`;

    try {
      const res = await fetch(url, {
        headers: { "X-Requested-With": "XMLHttpRequest" },
      });

      if (!res.ok) {
        window.location.href = fallbackHref;
        return;
      }

      const html = await res.text();
      openArticleModal(html);

      // URL güncelle (tam sayfa gitmeden)
      history.pushState({ articleModal: true }, "", `/Makaleler/${slug}`);
    } catch (err) {
      window.location.href = fallbackHref;
    }
  });

  // Geri tuşu: modal açıksa kapat
  window.addEventListener("popstate", function () {
    if (document.getElementById("article-modal")) {
      closeArticleModal(false); // popstate zaten oldu
    }
  });

  function openArticleModal(innerHtml) {
    // varsa temizle
    closeArticleModal(false);

    const modal = document.createElement("div");
    modal.id = "article-modal";
   modal.innerHTML = `
  <div class="article-modal-backdrop" data-close="1"></div>
  <div class="article-modal-panel" role="dialog" aria-modal="true">
    <div class="article-modal-header">
      <button class="article-modal-close" type="button" aria-label="Kapat" data-close="1">×</button>
    </div>
    <div class="article-modal-content">
      ${innerHtml}
    </div>
  </div>
`;


    document.body.appendChild(modal);
    document.body.classList.add("modal-open");

    // backdrop / close tıklaması
    modal.addEventListener("click", function (ev) {
      const t = ev.target;
      if (t && t.getAttribute("data-close") === "1") {
        closeArticleModal(true);
      }
    });

    // ESC
    document.addEventListener("keydown", onEscClose);
  }

  function onEscClose(e) {
    if (e.key === "Escape") {
      // modal açıksa kapat
      if (document.getElementById("article-modal")) closeArticleModal(true);
    }
  }

  function closeArticleModal(shouldGoBack) {
    const modal = document.getElementById("article-modal");
    if (!modal) return;

    modal.remove();
    document.body.classList.remove("modal-open");
    document.removeEventListener("keydown", onEscClose);

    if (shouldGoBack) {
      history.back(); // genelde /Makaleler sayfasına
    }
  }
})();