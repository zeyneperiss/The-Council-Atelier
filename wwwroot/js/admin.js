(function () {
  const shell = document.getElementById("adminShell");
  const toggle = document.getElementById("adminToggle");

  if (!shell || !toggle) return;

  // Sayfa yenilenince aynı kalsın
  const saved = localStorage.getItem("adminSidebarCollapsed");
  if (saved === "1") shell.classList.add("is-collapsed");

  toggle.addEventListener("click", () => {
    shell.classList.toggle("is-collapsed");
    localStorage.setItem(
      "adminSidebarCollapsed",
      shell.classList.contains("is-collapsed") ? "1" : "0"
    );
  });
})();