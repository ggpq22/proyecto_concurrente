package com.example.admin.pruebatracking.Entidades;

/**
 * Created by Mario on 25/10/2016.
 */
public class Cuenta extends DBEntidad{

    private String Usuario;

    private String Pass;

    public Cuenta(int Id, String Usuario, String Pass) {
        super(Id);
        this.Usuario = Usuario;
        this.Pass = Pass;
    }

    public String getUsuario() {
        return Usuario;
    }

    public void setUsuario(String usuario) {
        this.Usuario = usuario;
    }

    public String getPass() {
        return Pass;
    }

    public void setPass(String pass) {
        this.Pass = pass;
    }
}
