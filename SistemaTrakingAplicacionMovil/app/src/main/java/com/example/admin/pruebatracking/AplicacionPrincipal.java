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
    private boolean conectado = false;
    private boolean crearCuenta = false;
    private boolean respuestaCrearCuenta = false;
    private boolean login = false;
    private String cuenta = "";

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

    public boolean getCrearCuenta() {
        return crearCuenta;
    }

    public void setCrearCuenta(boolean crearCuenta){
        this.crearCuenta = crearCuenta;
    }

    public boolean getRespuestaCrearCuenta() {
        return respuestaCrearCuenta;
    }

    public void setRespuestaCrearCuenta(boolean respuestaCrearCuenta){
        this.respuestaCrearCuenta = respuestaCrearCuenta;
    }

    public boolean getLogin() {
        return login;
    }

    public void setLogin(boolean login){
        this.login = login;
    }

    public String getCuenta() {
        return cuenta;
    }

    public void setCuenta(String cuenta){
        this.cuenta = cuenta;
    }

}
