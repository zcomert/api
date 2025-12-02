```javascript
// 1. Status kodu 200 olmalı.
pm.test("Status code is 200", function () {
  pm.response.to.have.status(200);
});

// 2. JSON formatı doğrulaması
pm.test("Yanıt JSON formatında olmalı", function () {
  pm.response.to.be.withBody;
  pm.response.to.be.json;
});

// 3. Gecikme süresi doğrulaması
pm.test("Yanıt süresi 500 ms altında olmalı", function () {
  pm.expect(pm.response.responseTime).to.be.below(500);
});

// 4. Array olmalı
pm.test("Sonuç kümesi JSON array olmalı", function () {
  pm.response.to.be.withBody;
  pm.response.to.be.json;

  const data = pm.response.json();

  pm.expect(Array.isArray(data)).to.be.true;
});

// 5. one of
pm.test("Durum kodu 201 ya da 404 olmalı", function () {
  const status = pm.response.code;
  pm.expect([201, 404]).to.include(status);
});
```
