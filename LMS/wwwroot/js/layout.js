const appLayout = document.querySelector(".app-layout");
const sidebarToggle = document.getElementById("sidebar-toggle");

if (appLayout && sidebarToggle) {
    function applySidebarState(collapsed) {
        appLayout.classList.toggle(
            "is-sidebar-collapsed",
            collapsed
        );

        const label = collapsed
            ? "Mở rộng sidebar"
            : "Thu gọn sidebar";

        sidebarToggle.setAttribute(
            "aria-expanded",
            String(!collapsed)
        );

        sidebarToggle.setAttribute("aria-label", label);
        sidebarToggle.title = label;
    }

    // Đọc trạng thái đã lưu.
    let collapsed = false;

    try {
        collapsed =
            localStorage.getItem("lms.sidebar.collapsed") === "true";
    } catch {
        // Vẫn hoạt động nếu trình duyệt không cho phép lưu.
    }

    applySidebarState(collapsed);

    // Đổi trạng thái và lưu lại khi bấm nút.
    sidebarToggle.addEventListener("click", () => {
        const nextCollapsed =
            !appLayout.classList.contains("is-sidebar-collapsed");

        applySidebarState(nextCollapsed);

        try {
            localStorage.setItem(
                "lms.sidebar.collapsed",
                String(nextCollapsed)
            );
        } catch {
            // Chỉ bỏ qua việc lưu, vẫn thu gọn/mở rộng bình thường.
        }
    });
}