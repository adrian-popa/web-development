images = [
    'assets/images/billy-huynh-W8KTS-mhFUE-unsplash.jpg',
    'assets/images/cassi-josh-lhnOvu72BM8-unsplash.jpg',
    'assets/images/hao-wang-pVq6YhmDPtk-unsplash.jpg',
    'assets/images/lucas-benjamin-wQLAGv4_OYs-unsplash.jpg',
    'assets/images/pawel-czerwinski-tMbQpdguDVQ-unsplash.jpg',
    'assets/images/photo-boards-KZNTEn2r6tw-unsplash.jpg',
    'assets/images/rene-bohmer-YeUVDKZWSZ4-unsplash.jpg'
];

const gameCanvas = $('.game-canvas');
gameCanvas.css({position: 'relative'});

const scoreCount = $('.score-count');

let score = 0;

let gameOver = false;

function playGame() {
    if (!gameOver) {
        gameCanvas.empty();

        const imageSrc = images[Math.floor(Math.random() * images.length)];
        const imageWidth = Math.floor((Math.random() * 129) + 192 - score * 5);

        const img = $(`<img alt="Can't touch this" src="${imageSrc}" width="${imageWidth}">`);

        img.appendTo(gameCanvas);

        const posX = Math.floor(Math.random() * (gameCanvas.width() - img.width()));
        const posY = Math.floor(Math.random() * (gameCanvas.height() - img.height()));

        img.css({position: 'absolute', left: posX, top: posY, cursor: 'pointer'});
        img.attr('draggable', false);

        img.on('click', function () {
            score += 1;
            scoreCount.text(score);
            gameCanvas.empty();

            if (score === 10) {
                gameOver = true;

                const youWon = $('<h1 class="you-won">You Won!</h1>');
                youWon.appendTo('.game-container');
            }
        });

        scoreCount.text(score);

        const speed = Math.floor((Math.random() * 501) + 500 - score * 20);

        setTimeout(playGame, speed);
    }
}

$(window).on('load', function () {
    playGame();
});
