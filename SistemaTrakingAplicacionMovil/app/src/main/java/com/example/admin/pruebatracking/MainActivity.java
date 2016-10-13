package com.example.admin.pruebatracking;

import android.os.Bundle;
import android.app.Activity;
import android.view.View;
import android.view.View.OnClickListener;
import android.widget.Button;
import android.widget.EditText;
import android.widget.TextView;

public class MainActivity extends Activity {

    TextView response;
    EditText editTextAddress, editTextPort, editTextMensaje;
    Button buttonConnect, buttonClear;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);

        editTextAddress = (EditText) findViewById(R.id.ip);
        editTextPort = (EditText) findViewById(R.id.puerto);
        editTextMensaje = (EditText) findViewById(R.id.mensaje);
        buttonConnect = (Button) findViewById(R.id.enviar);
        buttonClear = (Button) findViewById(R.id.limpiar);
        response = (TextView) findViewById(R.id.respuesta);

        buttonConnect.setOnClickListener(new OnClickListener() {

            @Override
            public void onClick(View arg0) {
                Cliente myClient = new Cliente(editTextAddress.getText()
                        .toString(), Integer.parseInt(editTextPort
                        .getText().toString()), editTextMensaje.getText().toString(), response);
                myClient.execute();
            }
        });

        buttonClear.setOnClickListener(new OnClickListener() {

            @Override
            public void onClick(View v) {
                response.setText("");
            }
        });
    }

}