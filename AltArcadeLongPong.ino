int switch1 = 3;
int switch2 = 4;
int switch3 = 5;
int switch4 = 6;
int switch5 = 7;
bool turn;


void setup() {
  // put your setup code here, to run once:
  Serial.begin(9600);
  
  pinMode(switch1,INPUT);
  pinMode(switch2,INPUT);
  pinMode(switch3,INPUT);
  pinMode(switch4,INPUT);
  pinMode(switch5,INPUT);

}

void loop() {
  // put your main code here, to run repeatedly
turn = true;

   if(digitalRead(switch2) == HIGH && digitalRead(switch1) == LOW){
          Serial.flush();
          Serial.println(13);
          turn = false;
        }


            for(int i = 3; i<5; i++){
          if(digitalRead(switch3)==HIGH && digitalRead(i)==LOW){
              Serial.flush();
              Serial.println(14);
              turn = false;
            } 
        }


                for(int i = 3; i<6; i++){
          if(digitalRead(switch4)==HIGH && digitalRead(i)==LOW){
              Serial.flush();
              Serial.println(15);
              turn = false;
            } 
        }


 for(int i = 3; i<7; i++){
          if(digitalRead(switch5)==HIGH && digitalRead(i)==LOW){
              Serial.flush();
              Serial.println(16);
              turn = false;
            } 
        }

      

if(turn == true){
 
    if(digitalRead(switch1)==LOW && digitalRead(switch2)==LOW && digitalRead(switch3)==LOW && digitalRead(switch4)==LOW && digitalRead(switch5)==LOW){
    Serial.flush();
  Serial.println(0);
  }
     else if(digitalRead(switch1)==HIGH && digitalRead(switch2)==HIGH && digitalRead(switch3)==HIGH && digitalRead(switch4)==HIGH && digitalRead(switch5)==HIGH){
      Serial.flush();
      Serial.println(5);
    }
      else if(digitalRead(switch1)==HIGH && digitalRead(switch2)==HIGH && digitalRead(switch3)==HIGH && digitalRead(switch4)==HIGH){
      Serial.flush();
      Serial.println(4);
    }
      else if(digitalRead(switch1)==HIGH && digitalRead(switch2)==HIGH && digitalRead(switch3)==HIGH){
    Serial.flush();
    Serial.println(3);
  }
    else if(digitalRead(switch1)==HIGH && digitalRead(switch2)==HIGH){
    Serial.flush();
    Serial.println(2);
  }
   else if(digitalRead(switch1)==HIGH){
    Serial.flush();
    Serial.println(1);
  }


}
    
  delay(50);
}
