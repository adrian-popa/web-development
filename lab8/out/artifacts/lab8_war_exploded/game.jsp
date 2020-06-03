<%@ page import="weblab8.domain.User" %>
<%@ page contentType="text/html;charset=UTF-8" language="java" %>
<!DOCTYPE html>
<html>
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
    <title>Lab8 - Game</title>

    <link rel="stylesheet" href="styles.css">

    <script src="js/jquery-3.5.1.js"></script>
</head>
<body>

<%! User user; %>
<% user = (User) session.getAttribute("user");
    if (user != null) {
%>
<div class="game-container">
    <h3 id="gameInfo" style="margin-bottom: 20px;">Welcome, <%= user.getUsername() %>! Tap "join game" to start playing.</h3>
    <div id="gameCanvas">
        <button id="topLeft" type="button"></button>
        <button id="top" type="button"></button>
        <button id="topRight" type="button"></button>
        <button id="left" type="button"></button>
        <button id="center" type="button"></button>
        <button id="right" type="button"></button>
        <button id="bottomLeft" type="button"></button>
        <button id="bottom" type="button"></button>
        <button id="bottomRight" type="button"></button>
    </div>
    <button id="joinGameBtn" type="button">Join game</button>
</div>

<script>
    $(document).ready(function () {
        const username = '<%= user.getUsername() %>';

        let websocket = null;

        const gameCanvas = $('#gameCanvas').css('display', 'none');

        const board = [
            ['topLeft', 'top', 'topRight'],
            ['left', 'center', 'right'],
            ['bottomLeft', 'bottom', 'bottomRight']
        ];

        $('#joinGameBtn').click(function () {
            if ('WebSocket' in window) {
                if (username !== '') {
                    websocket = new WebSocket('ws://localhost:8080/game/' + username);
                    websocket.onopen = function (data) {
                        $('#gameInfo').text('Waiting for another player to join.')
                        $('#joinGameBtn').css('display', 'none');
                    };

                    websocket.onmessage = function (data) {
                        const message = data.data.split(":")[1];

                        if (message === 'joined') {
                            const playerTwo = data.data.split(":")[0];

                            $('#gameInfo').text('You\'re versing ' + playerTwo + '.')
                            gameCanvas.css('display', 'grid');
                        }

                        setMove(data.data);
                    };

                    websocket.onerror = function (e) {
                        alert('An error occurred, closing application');

                        cleanUp();
                    };

                    websocket.onclose = function (data) {
                        cleanUp();

                        const reason = data.reason ? data.reason : 'Goodbye';
                        alert(reason);
                    };
                } else {
                    alert('Username not provided');
                }
            } else {
                alert('WebSockets not supported');
            }
        });

        $('#gameCanvas button').click(function (event) {
            const move = event.target.id;

            const message = buildMessage(username, move);

            setMove(message);
            websocket.send(message);
        });

        function buildMessage(username, message) {
            return username + ':' + message;
        }

        function setMove(msg) {
            const msgUsername = msg.split(':')[0];
            const move = msg.split(':')[1];

            const isPlayerOne = msgUsername === username;

            const moveButton = $('#' + move);
            moveButton.text(isPlayerOne ? 'X' : 'O');

            if (isGameOver()) {
                $('#gameInfo').text(isPlayerOne ? 'You won!' : 'You lost!')
                gameCanvas.css('display', 'none');
            } else if (isDraw()) {
                $('#gameInfo').text('It\'s a draw!')
                gameCanvas.css('display', 'none');
            }
        }

        function isGameOver() {
            // check columns
            for(let i = 0; i < 3; i++){
                const left = $('#' + board[i][0]).text();
                const center = $('#' + board[i][1]).text();
                const right = $('#' + board[i][2]).text();

                if (left === center && left === right && left !== '') {
                    return true;
                }
            }

            // check rows
            for(let i = 0; i < 3; i++){
                const top = $('#' + board[0][i]).text();
                const center = $('#' + board[1][i]).text();
                const bottom = $('#' + board[2][i]).text();

                if (top === center && top === bottom && top !== '') {
                    return true;
                }
            }

            // check primary diagonal
            const topLeft = $('#' + board[0][0]).text();
            const center = $('#' + board[1][1]).text();
            const bottomRight = $('#' + board[2][2]).text();

            if (topLeft === center && topLeft === bottomRight && topLeft !== '') {
                return true;
            }

            // check secondary diagonal
            const topRight = $('#' + board[0][2]).text();
            const bottomLeft = $('#' + board[2][0]).text();

            if (topRight === center && topRight === bottomLeft && topRight !== '') {
                return true;
            }

            return false;
        }

        function isDraw() {
            for (let i = 0; i < 3; i++) {
                for (let j = 0; j < 3; j++) {
                    if ($('#' + board[i][j]).text() === '') {
                        return false;
                    }
                }
            }

            return true;
        }

        function cleanUp() {
            gameCanvas.css('display', 'none');

            $('#gameInfo').text('Disconnected from game session, try again.')

            websocket = null;
        }
    });
</script>

<%
    }
%>

</body>
</html>
