package com.example.admin.pruebatracking;

import android.app.Application;
import android.content.Context;

import com.example.admin.pruebatracking.Entidades.Cuenta;
import com.example.admin.pruebatracking.Entidades.Grupo;
import com.example.admin.pruebatracking.IU.FragmentChats;
import com.example.admin.pruebatracking.IU.ListViewAdapterGrupos;
import com.example.admin.pruebatracking.Mensajes.MsgDBRespuesta;

import java.net.Socket;
import java.util.ArrayList;

/**
 * Created by Admin on 8/11/2016.
 */
public class AplicacionPrincipal extends Application {

    private Socket socket;
    private Context context;
    MsgDBRespuesta msgDBRespuestaEntrar;
    private boolean conectado = false;
    private boolean crearCuenta = false;
    private boolean respuestaEntrar = false;
    private boolean login = false;
    private boolean recuperarGrupos;
    private boolean respuestaRecuperarGrupos;
    private Cuenta cuenta;
    private ArrayList<Grupo> grupos = new ArrayList<Grupo>();
    private ListViewAdapterGrupos adapterGrupos;

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

    public boolean getRecuperarGrupos() {
        return recuperarGrupos;
    }

    public void setRecuperarGrupos(boolean recuperarGrupos) {
        this.recuperarGrupos = recuperarGrupos;
    }

    public boolean getRespuestaRecuperarGrupos() {
        return respuestaRecuperarGrupos;
    }

    public void setRespuestaRecuperarGrupos(boolean respuestaRecuperarGrupos) {
        this.respuestaRecuperarGrupos = respuestaRecuperarGrupos;
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

    public ArrayList<Grupo> getGrupos() {
        return grupos;
    }

    public void setGrupos(ArrayList<Grupo> grupos) {
        this.grupos = grupos;
    }

    public ListViewAdapterGrupos getAdapterGrupos() {
        return adapterGrupos;
    }

    public void setAdapterGrupos(ListViewAdapterGrupos adapterGrupos) {
        this.adapterGrupos = adapterGrupos;
    }

    public Context getContext() {
        return context;
    }

    public void setContext(Context context) {
        this.context = context;
    }
}
