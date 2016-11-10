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
import com.example.admin.pruebatracking.Entidades.DBEntidad;
import com.example.admin.pruebatracking.Mensajes.MsgDBPeticion;
import com.example.admin.pruebatracking.Mensajes.MsgDBRespuesta;
import com.example.admin.pruebatracking.R;

import java.net.Socket;
import java.util.ArrayList;
import java.util.UUID;

public class SignupActivity extends AppCompatActivity {
    private static final String TAG = "SignupActivity";

    EditText _nombreText;
    EditText _apellidoText;
    EditText _emailText;
    EditText _passwordText;
    EditText _rePasswordText;
    Button _signupButton;
    TextView _loginLink;
    
    @Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_signup);

        _nombreText = (EditText)findViewById(R.id.input_nombre);
        _apellidoText = (EditText)findViewById(R.id.input_apellido);
        _emailText = (EditText)findViewById(R.id.input_email);
        _passwordText = (EditText)findViewById(R.id.input_password);
        _rePasswordText = (EditText)findViewById(R.id.input_rePassword);
        _signupButton = (Button)findViewById(R.id.btn_signup);
        _loginLink = (TextView)findViewById(R.id.link_login);

        _signupButton.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                signup();
            }
        });

        _loginLink.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                Intent intent = new Intent(getApplicationContext(),LoginActivity.class);
                startActivity(intent);
                finish();
                overridePendingTransition(R.anim.push_left_in, R.anim.push_left_out);
            }
        });
    }

    public void signup() {
        Log.d(TAG, "Signup");

        if (!validate()) {
            onSignupFailed();
            return;
        }

        _signupButton.setEnabled(false);

        final ProgressDialog progressDialog = new ProgressDialog(SignupActivity.this,
                R.style.AppTheme_Dark_Dialog);
        progressDialog.setIndeterminate(true);
        progressDialog.setMessage("Creando cuenta...");
        progressDialog.setCancelable(false);
        progressDialog.show();

        String nombre = _nombreText.getText().toString();
        String apellido = _apellidoText.getText().toString();
        final String email = _emailText.getText().toString();
        final String password = _passwordText.getText().toString();
        String rePassword = _rePasswordText.getText().toString();

        new android.os.Handler().postDelayed(
                new Runnable() {
                    public void run() {
                        String  uniqueID = UUID.randomUUID().toString();
                        ArrayList<String> arrayDestino = new ArrayList<String>();
                        arrayDestino.add(uniqueID);

                        String fecha = (DateFormat.format("yyyy-MM-dd", new java.util.Date()).toString());

                        Cuenta cuenta = new Cuenta(1,email, password);
                        ArrayList<DBEntidad> arrayCuenta = new ArrayList<DBEntidad>();
                        arrayCuenta.add(cuenta);

                        Cliente cliente = new Cliente(getApplicationContext(),arrayDestino,uniqueID, fecha);
                        cliente.execute();
                        

                        cliente.recibirMensajes();

                        MsgDBPeticion peticion = new MsgDBPeticion(arrayDestino, uniqueID, fecha, "CrearCuenta", arrayCuenta, false);
                        cliente.enviarMensajes(peticion);

                        try {
                            this.wait();
                            MsgDBRespuesta msg = ((AplicacionPrincipal) getApplicationContext()).getMsgRespuesta();
                            if(msg != null && msg.getIsValido())
                            {
                                onSignupSuccess();
                            }
                            else
                            {
                                onSignupFailed();
                            }
                            progressDialog.dismiss();
                        }
                        catch (Exception e)
                        {
                            e.printStackTrace();
                            onSignupFailed();
                        }
                    }
                }, 2500);
    }


    public void onSignupSuccess() {
        _signupButton.setEnabled(true);
        setResult(RESULT_OK, null);
        finish();
    }

    public void onSignupFailed() {
        Toast.makeText(getBaseContext(), "Verifique los datos ingresados", Toast.LENGTH_SHORT).show();
        _signupButton.setEnabled(true);
    }

    public boolean validate() {
        boolean valid = true;

        String nombre = _nombreText.getText().toString();
        String apellido = _apellidoText.getText().toString();
        String email = _emailText.getText().toString();
        String password = _passwordText.getText().toString();
        String rePassword = _rePasswordText.getText().toString();

        if (nombre.isEmpty() || nombre.length() < 3) {
            _nombreText.setError("Ingrese un nombre valido");
            valid = false;
        } else {
            _nombreText.setError(null);
        }

        if (apellido.isEmpty() || nombre.length() < 3) {
            _apellidoText.setError("Ingrese un apellido valido");
            valid = false;
        } else {
            _apellidoText.setError(null);
        }

        if (email.isEmpty() || !android.util.Patterns.EMAIL_ADDRESS.matcher(email).matches()) {
            _emailText.setError("Ingrese un email valido");
            valid = false;
        } else {
            _emailText.setError(null);
        }

        if (password.isEmpty() || password.length() < 4 || password.length() > 10) {
            _passwordText.setError("entre 4 y 10 caracteres");
            valid = false;
        } else {
            _passwordText.setError(null);
        }

        if (rePassword.isEmpty() || rePassword.length() < 4 || rePassword.length() > 10 || !(rePassword.equals(password))) {
            _rePasswordText.setError("El password no coincide");
            valid = false;
        } else {
            _rePasswordText.setError(null);
        }

        return valid;
    }

    @Override
    public void onBackPressed() {
        Intent intent = new Intent(getApplicationContext(),LoginActivity.class);
        startActivity(intent);
        finish();
        overridePendingTransition(R.anim.push_left_in, R.anim.push_left_out);
    }
}