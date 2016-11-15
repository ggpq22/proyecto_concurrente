package com.example.admin.pruebatracking.IU;

import android.app.ProgressDialog;
import android.content.Intent;
import android.os.Bundle;
import android.support.v7.app.AppCompatActivity;
import android.text.format.DateFormat;
import android.util.Log;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.TextView;
import android.widget.Toast;

import com.example.admin.pruebatracking.AplicacionPrincipal;
import com.example.admin.pruebatracking.Cliente;
import com.example.admin.pruebatracking.Entidades.Cuenta;
import com.example.admin.pruebatracking.Entidades.Grupo;
import com.example.admin.pruebatracking.Mensajes.MsgDBPeticion;
import com.example.admin.pruebatracking.Mensajes.MsgDBRespuesta;
import com.example.admin.pruebatracking.R;

import java.util.ArrayList;
import java.util.UUID;

public class LoginActivity extends AppCompatActivity {
    private static final String TAG = "LoginActivity";
    private static final int REQUEST_SIGNUP = 0;

    EditText _emailText;
    EditText _passwordText;
    Button _loginButton;
    TextView _signupLink;
    
    @Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_login);

        _emailText = (EditText)findViewById(R.id.input_email);
        _passwordText = (EditText)findViewById(R.id.input_password);
        _loginButton = (Button)findViewById(R.id.btn_login);
        _signupLink = (TextView)findViewById(R.id.link_signup);
        
        _loginButton.setOnClickListener(new View.OnClickListener() {

            @Override
            public void onClick(View v) {
                login();
            }
        });

        _signupLink.setOnClickListener(new View.OnClickListener() {

            @Override
            public void onClick(View v) {
                // Start the Signup activity
                Intent intent = new Intent(getApplicationContext(), SignupActivity.class);
                startActivityForResult(intent, REQUEST_SIGNUP);
                finish();
                overridePendingTransition(R.anim.push_left_in, R.anim.push_left_out);
            }
        });


    }

    public void login() {
        Log.d(TAG, "Login");

        if (!validate()) {
            onLoginFailed();
            return;
        }

        _loginButton.setEnabled(false);

        final ProgressDialog progressDialog = new ProgressDialog(LoginActivity.this, R.style.AppTheme_Dark_Dialog);
        progressDialog.setIndeterminate(true);
        progressDialog.setMessage("Autentificando...");
        progressDialog.setCancelable(false);
        progressDialog.show();

        final String email = _emailText.getText().toString();
        final String password = _passwordText.getText().toString();

        new Thread() {
            public void run() {

                String uniqueID = UUID.randomUUID().toString();
                ArrayList<String> arrayDestino = new ArrayList<String>();
                arrayDestino.add(uniqueID);

                String fecha = (DateFormat.format("yyyy-MM-dd", new java.util.Date()).toString());

                Cuenta cuenta = new Cuenta(1, email, password, 0);
                ArrayList<Cuenta> arrayCuenta = new ArrayList<Cuenta>();
                arrayCuenta.add(cuenta);

                Cliente cliente = new Cliente(getBaseContext(), arrayDestino, uniqueID, fecha);
                cliente.execute();

                while (!((AplicacionPrincipal) getApplicationContext()).getConectado()) {
                    try {
                        Thread.sleep(1);
                    } catch (Exception e) {
                        e.printStackTrace();
                        Log.e("msg", "Error en esperando conexion: " + e.toString());
                    }
                }

                Log.e("msg", "PASO EN CREAR CONEXION");

                MsgDBPeticion peticion = new MsgDBPeticion(arrayDestino, uniqueID, fecha, "Login", arrayCuenta, new ArrayList<Grupo>(), false);
                cliente.enviarMensajes(peticion);

                while (!((AplicacionPrincipal) getApplicationContext()).getLogin()) {
                    try {
                        Thread.sleep(1);
                    } catch (Exception e) {
                        e.printStackTrace();
                        Log.e("msg", "Error en esperando enviar peticion Crear Cuenta: " + e.toString());
                    }
                }

                Log.e("msg", "PASO PETICION LOGIN");

                while (!((AplicacionPrincipal) getApplicationContext()).getRespuestaEntrar()) {
                    try {
                        Thread.sleep(1);
                    } catch (Exception e) {
                        e.printStackTrace();
                        Log.e("msg", "Error en esperando Respuesta Login: " + e.toString());
                    }
                }

                cliente.pararRecibirMensajes();
                cliente.cancel(true
                );
                cliente.cerrarConexion();
                Log.e("msg", "PASO RESPUESTA LOGIN");

                ((AplicacionPrincipal) getApplicationContext()).setLogin(false);
                ((AplicacionPrincipal) getApplicationContext()).setRespuestaEntrar(false);
                ((AplicacionPrincipal) getApplicationContext()).setConectado(false);

                MsgDBRespuesta msg = ((AplicacionPrincipal) getApplicationContext()).getMsgDBRespuestaEntrar();
                if (msg != null && msg.getIsValido()) {
                    ((AplicacionPrincipal) getApplicationContext()).setCuenta(msg.getReturnCuenta().get(0));
                    cuenta = ((AplicacionPrincipal) getBaseContext().getApplicationContext()).getCuenta();

                    arrayDestino = new ArrayList<String>();
                    arrayDestino.add(cuenta.getUsuario());
                    fecha = (DateFormat.format("yyyy-MM-dd", new java.util.Date()).toString());

                    arrayCuenta = new ArrayList<Cuenta>();
                    arrayCuenta.add(cuenta);

                    cliente = new Cliente(getBaseContext(), arrayDestino, cuenta.getUsuario(), fecha);
                    cliente.execute();

                    while (!((AplicacionPrincipal) getApplicationContext()).getConectado()) {
                        try {
                            Thread.sleep(1);
                        } catch (Exception e) {
                            e.printStackTrace();
                            Log.e("msg", "Error en esperando conexion con usuario: " + e.toString());
                        }
                    }

                    Log.e("msg", "PASO CREAR CONEXION CON USUARIO");

                    MsgDBPeticion peticionGrupos = new MsgDBPeticion(arrayDestino,((AplicacionPrincipal) getBaseContext().getApplicationContext()).getCuenta().getUsuario(), fecha, "GetGrupoPorIntegrante", arrayCuenta, new ArrayList<Grupo>(), false);
                    cliente.enviarMensajes(peticionGrupos);

                    while (!((AplicacionPrincipal) getApplicationContext()).getRecuperarGrupos()) {
                        try {
                            Thread.sleep(1);
                        } catch (Exception e) {
                            e.printStackTrace();
                            Log.e("msg", "Error en esperando enviar peticion Recuperar Grupos: " + e.toString());
                        }
                    }

                    Log.e("msg", "PASO PETICION RECUPERAR GRUPOS");

                    while (!((AplicacionPrincipal) getApplicationContext()).getRespuestaRecuperarGrupos()) {
                        try {
                            Thread.sleep(1);
                        } catch (Exception e) {
                            e.printStackTrace();
                            Log.e("msg", "Error en esperando Respuesta Login: " + e.toString());
                        }
                    }

                    Log.e("msg", "PASO RESPUESTA RECUPERAR GRUPOS");

                    ((AplicacionPrincipal) getApplicationContext()).setRecuperarGrupos(false);
                    ((AplicacionPrincipal) getApplicationContext()).setRespuestaRecuperarGrupos(false);

                    onLoginSuccess();
                } else {
                    onLoginFailed();
                }

                runOnUiThread(new Runnable() {
                    @Override
                    public void run() {
                        progressDialog.dismiss();
                    }
                });

            }
        }.start();
    }


    @Override
    protected void onActivityResult(int requestCode, int resultCode, Intent data) {
        if (requestCode == REQUEST_SIGNUP) {
            if (resultCode == RESULT_OK) {
                this.finish();
            }
        }
    }

    @Override
    public void onBackPressed() {
        moveTaskToBack(true);
    }

    public void onLoginSuccess() {
        runOnUiThread(new Runnable() {
            @Override
            public void run() {
                _loginButton.setEnabled(true);
                finish();
            }
        });
    }

    public void onLoginFailed() {
        runOnUiThread(new Runnable() {
            @Override
            public void run() {
                Toast.makeText(getBaseContext(), "No se pudo ingresar, Verifique sus datos", Toast.LENGTH_LONG).show();
                _loginButton.setEnabled(true);
            }
        });
    }

    public boolean validate() {
        boolean valid = true;

        String email = _emailText.getText().toString();
        String password = _passwordText.getText().toString();

        if (email.isEmpty() || !android.util.Patterns.EMAIL_ADDRESS.matcher(email).matches()) {
            _emailText.setError("enter a valid email address");
            valid = false;
        } else {
            _emailText.setError(null);
        }

        if (password.isEmpty() || password.length() < 4 || password.length() > 10) {
            _passwordText.setError("between 4 and 10 alphanumeric characters");
            valid = false;
        } else {
            _passwordText.setError(null);
        }

        return valid;
    }
}
