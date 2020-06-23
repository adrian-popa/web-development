//package weblab8.controller;
//
//import weblab8.domain.Message;
//import weblab8.utils.GameSessionManager;
//import weblab8.utils.MessageDecoder;
//import weblab8.utils.MessageEncoder;
//
//import javax.servlet.http.HttpServlet;
//import javax.websocket.CloseReason.CloseCodes;
//import javax.websocket.*;
//import javax.websocket.server.PathParam;
//import javax.websocket.server.ServerEndpoint;
//import java.util.Objects;
//
//@ServerEndpoint(value = "/game/{username}", encoders = MessageEncoder.class, decoders = MessageDecoder.class)
//public final class GameController extends HttpServlet {
//
//    public GameController() {
//        super();
//    }
//
//    @OnOpen
//    public void onOpen(@PathParam("username") final String username, final Session session) {
//        if (Objects.isNull(username) || username.isEmpty()) {
//            throw new RegistrationFailedException("User name is required");
//        } else {
//            session.getUserProperties().put("username", username);
//            if (GameSessionManager.register(session)) {
//                System.out.printf("Session opened for %s\n", username);
//
//                GameSessionManager.publish(new Message((String) session.getUserProperties().get("username"), "joined"), session);
//            } else {
//                throw new RegistrationFailedException("Unable to join, you're using the same account or there is a game already in progress, please try again later.");
//            }
//        }
//    }
//
//    @OnError
//    public void onError(final Session session, final Throwable throwable) {
//        if (throwable instanceof RegistrationFailedException) {
//            GameSessionManager.close(session, CloseCodes.VIOLATED_POLICY, throwable.getMessage());
//        }
//    }
//
//    @OnMessage
//    public void onMessage(final Message message, final Session session) {
//        GameSessionManager.publish(message, session);
//    }
//
//    @OnClose
//    public void onClose(final Session session) {
//        if (GameSessionManager.remove(session)) {
//            System.out.printf("Session closed for %s\n", session.getUserProperties().get("username"));
//
//            GameSessionManager.publish(new Message((String) session.getUserProperties().get("username"), "quit"), session);
//        }
//    }
//
//    private static final class RegistrationFailedException extends RuntimeException {
//
//        private static final long serialVersionUID = 1L;
//
//        public RegistrationFailedException(final String message) {
//            super(message);
//        }
//    }
//}
