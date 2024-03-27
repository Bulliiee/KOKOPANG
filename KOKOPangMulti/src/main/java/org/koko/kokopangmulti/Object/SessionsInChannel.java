package org.koko.kokopangmulti.Object;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import java.util.ArrayList;
import java.util.stream.Collectors;
import java.util.stream.Stream;

public class SessionsInChannel {
    private static final Logger log = LoggerFactory.getLogger(SessionsInChannel.class);
    private int cnt;
    private ArrayList<Integer> isExisted;
    Integer[] init = new Integer[]{1, 0, 0, 0, 0, 0};

    public SessionsInChannel() {
        this.cnt = 1;
        this.isExisted = new ArrayList<>(Stream.of(init)
                .collect(Collectors.toList()));
    }

    public Integer getCnt() {
        return this.cnt;
    }

    public ArrayList<Integer> getIsExisted() {
        return this.isExisted;
    }
    public void plusCnt() {
        this.cnt+=1;
    }

    public void minusCnt() {
        this.cnt-=1;
    }

    public void setFalseIsExisted(int idx){
        if (idx<0 || idx>=6) {
            log.warn("[OUT OF RANGE] CHECK INDEX");
            return;
        }
        this.isExisted.set(idx, 0);
    }

    public void setTrueIsExisted(int idx){
        this.isExisted.set(idx, 1);
    }

}
