package com.example.admin.pruebatracking;

import android.app.Application;

import com.example.admin.pruebatracking.Entidades.Cuenta;
import com.example.admin.pruebatracking.Mensajes.MsgDBRespuesta;

import java.net.Socket;

/**
 * Created by Admin on 8/11/2016.
 */
public class AplicacionPrincipal extends Application {

    private Socket socket;
    MsgDBRespuesta msgDBRespuestaEntrar;
    private boolean conectado = false;
    private boolean crearCuenta = false;
    private boolean respuestaEntrar = false;
    private boolean login = false;
    private Cuenta cuenta;

    public Socket getSocket() {
        return socket;
    }

    public void setSocket(Socket socket)
    {
        this.socket = socket;
    }

    public MsgDBRespuesta getMsgDBRespuestaEntrar() {
        return msgDBRespuestaEntrar;
    }

    public void setMsgDBRespuestaEntrar(MsgDBRespuesta msgDBRespuestaEntrar) {
        this.msgDBRespuestaEntrar = msgDBRespuestaEntrar;
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

    public boolean getRespuestaEntrar() {
        return respuestaEntrar;
    }

    public void setRespuestaEntrar(boolean respuestaEntrar){
        this.respuestaEntrar = respuestaEntrar;
    }

    public boolean getLogin() {
        return login;
    }

    public void setLogin(boolean login){
        this.login = login;
    }

    public Cuenta getCuenta() {
        return cuenta;
    }

    public void setCuenta(Cuenta cuenta){
        this.cuenta = cuenta;
    }

}
