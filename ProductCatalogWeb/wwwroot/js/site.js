// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function addTooltipRateListener(apiUrl) {
    document.addEventListener("DOMContentLoaded", function () {
        const tooltips = document.querySelectorAll('#tooltip');

        tooltips.forEach(tooltip => {
            let tooltipInstance = null;

            tooltip.addEventListener('mouseenter', function () {
                const rubPrice = parseFloat(tooltip.previousElementSibling.textContent.trim().replace(',', ''));
                fetch(apiUrl + "/api/rate")
                    .then(response => response.json())
                    .then(data => {
                        const usdRate = parseFloat(data.result);
                        const usdPrice = rubPrice / usdRate;
                        tooltip.setAttribute('data-bs-title', '$' + usdPrice.toFixed(2));

                        if (!tooltipInstance) {
                            tooltipInstance = new bootstrap.Popover(tooltip, {
                                trigger: 'manual'
                            });
                        }
                        tooltipInstance.show();
                    })
                    .catch(error => {
                        console.error('Ошибка при получении курса обмена:', error);
                    });
            });

            tooltip.addEventListener('mouseleave', function () {
                if (tooltipInstance) {
                    tooltipInstance.hide();
                }
            });
        });
    });
}
