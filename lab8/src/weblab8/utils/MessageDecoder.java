package weblab8.utils;

import weblab8.domain.Message;

import javax.websocket.Decoder;
import javax.websocket.EndpointConfig;

public final class MessageDecoder implements Decoder.Text<Message> {

    @Override
    public void destroy() {
    }

    @Override
    public void init(final EndpointConfig arg0) {
    }

    @Override
    public Message decode(final String arg0) {
        String username = arg0.split(":")[0];
        String message = arg0.split(":")[1];
        return new Message(username, message);
    }

    @Override
    public boolean willDecode(final String arg0) {
        return arg0.contains(":");
    }
}
