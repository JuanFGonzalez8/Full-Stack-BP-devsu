import { ComponentFixture, TestBed } from '@angular/core/testing';
import { FormClienteComponent } from '../../clientes/form-cliente/form-cliente';

describe('Formulario', () => {
  let component: FormClienteComponent;
  let fixture: ComponentFixture<FormClienteComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [FormClienteComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(FormClienteComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
