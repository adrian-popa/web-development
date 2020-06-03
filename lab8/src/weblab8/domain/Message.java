package weblab8.domain;

import com.fasterxml.jackson.annotation.JsonCreator;
import com.fasterxml.jackson.annotation.JsonProperty;

import java.util.Objects;

public final class Message {

    @JsonProperty
    private final String username;
    @JsonProperty
    private final String message;

    @JsonCreator
    public Message(@JsonProperty("username") final String username, @JsonProperty("message") final String message) {
        Objects.requireNonNull(username);
        Objects.requireNonNull(message);

        this.username = username;
        this.message = message;
    }

    String getUsername() {
        return this.username;
    }

    String getMessage() {
        return this.message;
    }

    @Override
    public String toString() {
        return username + ":" + message;
    }
}
