package com.example.admin.pruebatracking;

import android.app.Application;

import com.example.admin.pruebatracking.Mensajes.MsgDBRespuesta;

import java.net.Socket;

/**
 * Created by Admin on 8/11/2016.
 */
public class AplicacionPrincipal extends Application {

    private MsgDBRespuesta msgRespuesta;
    private Socket socket;
    private boolean conectado;

    public MsgDBRespuesta getMsgRespuesta() {
        return msgRespuesta;
    }

    public void setMsgRespuesta(MsgDBRespuesta msgRespuesta) {
        this.msgRespuesta = msgRespuesta;
    }

    public Socket getSocket() {
        return socket;
    }

    public void setSocket(Socket socket)
    {
        this.socket = socket;
    }
    public boolean getConectado() {
        return conectado;
    }

    public void setConectado(boolean conectado){
        this.conectado = conectado;
    }
}
