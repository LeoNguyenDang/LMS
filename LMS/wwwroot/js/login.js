const passwordInput = document.getElementById("Password");
const toggleButton = document.getElementById("toggle-password");

if (passwordInput && toggleButton) {
    toggleButton.hidden = false;

    toggleButton.addEventListener("click", () => {
        const showPassword = passwordInput.type === "password";

        passwordInput.type = showPassword ? "text" : "password";

        toggleButton.setAttribute(
            "aria-label",
            showPassword ? "Ẩn mật khẩu" : "Hiện mật khẩu"
        );

        toggleButton.setAttribute(
            "aria-pressed",
            String(showPassword)
        );
    });
}