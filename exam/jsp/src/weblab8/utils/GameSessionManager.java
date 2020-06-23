//package weblab8.utils;
//
//import weblab8.domain.Message;
//
//import javax.websocket.CloseReason;
//import javax.websocket.CloseReason.CloseCodes;
//import javax.websocket.EncodeException;
//import javax.websocket.Session;
//import java.io.IOException;
//import java.util.Objects;
//import java.util.Set;
//import java.util.concurrent.CopyOnWriteArraySet;
//import java.util.concurrent.locks.Lock;
//import java.util.concurrent.locks.ReentrantLock;
//
//public final class GameSessionManager {
//
//    private static final Lock LOCK = new ReentrantLock();
//    private static final Set<Session> SESSIONS = new CopyOnWriteArraySet<>();
//
//    private static String firstUserConnected;
//    private static String secondUserConnected;
//
//    private GameSessionManager() {
//        throw new IllegalStateException("Instantiation not allowed");
//    }
//
//    public static void publish(final Message message, final Session origin) {
//        assert !Objects.isNull(message) && !Objects.isNull(origin);
//
//        SESSIONS.stream().filter(session -> !session.equals(origin)).forEach(session -> {
//            try {
//                session.getBasicRemote().sendObject(message);
//            } catch (IOException | EncodeException e) {
//                e.printStackTrace();
//            }
//        });
//    }
//
//    public static boolean register(final Session session) {
//        assert !Objects.isNull(session);
//
//        if (secondUserConnected != null && !secondUserConnected.isEmpty()) {
//            return false;
//        }
//
//        boolean result = false;
//        try {
//            LOCK.lock();
//
//            result = !SESSIONS.contains(session) && SESSIONS.stream()
//                    .noneMatch(elem -> elem.getUserProperties().get("username").equals(session.getUserProperties().get("username"))) && SESSIONS.add(session);
//
//            if (firstUserConnected != null && !firstUserConnected.isEmpty()) {
//                Message message = new Message(firstUserConnected, "joined");
//                try {
//                    session.getBasicRemote().sendObject(message);
//                } catch (IOException | EncodeException e) {
//                    e.printStackTrace();
//                }
//
//                secondUserConnected = (String) session.getUserProperties().get("username");
//            }
//
//            firstUserConnected = (String) session.getUserProperties().get("username");
//        } finally {
//            LOCK.unlock();
//        }
//
//        return result;
//    }
//
//    public static void close(final Session session, final CloseCodes closeCode, final String message) {
//        assert !Objects.isNull(session) && !Objects.isNull(closeCode);
//
//        try {
//            session.close(new CloseReason(closeCode, message));
//        } catch (IOException e) {
//            throw new RuntimeException("Unable to close session", e);
//        }
//
//        cleanUp(session);
//    }
//
//    public static boolean remove(final Session session) {
//        assert !Objects.isNull(session);
//
//        cleanUp(session);
//
//        return SESSIONS.remove(session);
//    }
//
//    private static void cleanUp(final Session session) {
//        if (firstUserConnected != null && session.getUserProperties().get("username").equals(firstUserConnected)
//                && !firstUserConnected.equals(secondUserConnected)) {
//            firstUserConnected = null;
//        }
//
//        if (secondUserConnected != null && session.getUserProperties().get("username").equals(secondUserConnected)
//                && !firstUserConnected.equals(secondUserConnected)) {
//            secondUserConnected = null;
//        }
//    }
//}
