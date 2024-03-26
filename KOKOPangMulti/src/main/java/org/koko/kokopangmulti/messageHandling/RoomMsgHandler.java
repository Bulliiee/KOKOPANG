package org.koko.kokopangmulti.messageHandling;

import com.fasterxml.jackson.core.JsonProcessingException;
import com.fasterxml.jackson.core.type.TypeReference;
import com.fasterxml.jackson.databind.ObjectMapper;
import org.json.JSONException;
import org.json.JSONObject;
import org.koko.kokopangmulti.Channel.ChannelHandler;

import java.util.Map;

public class RoomMsgHandler {
    ObjectMapper objectMapper;

    public void filterData(String userName, JSONObject data) throws JSONException {

        String type = data.getString("type");

        if (type.equals("create")) {
            String channelName = data.getString("channelName");
            ChannelHandler.createChannel(userName, channelName);
        } else if (type.equals("join")) {
            int channelIndex = Integer.parseInt(data.getString("channelIndex"));
            ChannelHandler.joinChannel(userName, channelIndex);
        }
    }

}
